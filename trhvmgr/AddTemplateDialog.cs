using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using trhvmgr.Lib;
using trhvmgr.Objects;
using trhvmgr.Plugs;
using trhvmgr.Properties;
using trhvmgr.UI;

namespace trhvmgr
{
    public partial class AddTemplateDialog : Form
    {
        private class SwitchObj
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public override string ToString()
            {
                return Name + " [" + Type + "]";
            }
        }

        private List<HostComputer> hostComputers = new List<HostComputer>();
        private List<VirtualMachine> virtualMachines = new List<VirtualMachine>();
        private JsonHelper jsonHelper = null;

        public AddTemplateDialog()
        {
            InitializeComponent();
            jsonHelper = new JsonHelper(Settings.Default.templateFile, FileShare.Read);
            serverComboBox.ComboBox.SelectedIndexChanged += (o, e) =>
            {
                adapterComboBox.ComboBox.Items.Clear();
                adapterComboBox.ComboBox.Items.AddRange(
                    HyperV.GetVmSwitch(GetSelectedHost()?.HostName)
                    .Select(x => new SwitchObj {
                        Name = (string) x.Members["Name"].Value,
                        Type = (string) x.Members["SwitchType"].Value
                    }).ToArray());
            };
        }

        #region Private Methods

        private HostComputer GetSelectedHost()
        {
            if(serverComboBox.ComboBox.SelectedIndex >= 0 && serverComboBox.ComboBox.SelectedIndex < hostComputers.Count())
                return hostComputers[serverComboBox.ComboBox.SelectedIndex];
            return null;
        }

        private VirtualMachine GetSelectedVirtualMachine()
        {
            if (baseComboBox.ComboBox.SelectedIndex >= 0 && baseComboBox.ComboBox.SelectedIndex < virtualMachines.Count())
                return virtualMachines[baseComboBox.ComboBox.SelectedIndex];
            return null;
        }

        private SwitchObj GetSelectedSwitch() => adapterComboBox.ComboBox.SelectedItem as SwitchObj;

        #endregion

        #region UI Events

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return handleParam;
            }
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            bool isFormValid = false;
            // Disgusting
            if (serverComboBox.SelectedIndex == -1) serverComboBox.IsValid = tribool.FALSE; else serverComboBox.IsValid = tribool.TRUE;
            if (baseComboBox.SelectedIndex == -1) baseComboBox.IsValid = tribool.FALSE; else baseComboBox.IsValid = tribool.TRUE;
            if (configComboBox.SelectedIndex == -1) configComboBox.IsValid = tribool.FALSE; else configComboBox.IsValid = tribool.TRUE;
            if (adapterComboBox.SelectedIndex == -1) adapterComboBox.IsValid = tribool.FALSE; else adapterComboBox.IsValid = tribool.TRUE;
            if (string.IsNullOrEmpty(vmTextbox.Text)) vmTextbox.IsValid = tribool.FALSE; else vmTextbox.IsValid = tribool.TRUE;

            isFormValid = serverComboBox.IsValid == tribool.TRUE
                && baseComboBox.IsValid == tribool.TRUE
                && vmTextbox.IsValid == tribool.TRUE
                && configComboBox.IsValid == tribool.TRUE
                && adapterComboBox.IsValid == tribool.TRUE;

            if (!isFormValid) this.DialogResult = DialogResult.None;
            else
            {
                // !! IMPORTANT !!
                // We NEED these exactly here because of cross thread errors
                // (we cannot get the user inputs from the BackgroundWorker)
                var a1 = GetSelectedHost().HostName;
                var a2 = vmTextbox.Text;
                var a3 = GetSelectedVirtualMachine().Uuid;
                var a4 = GetSelectedSwitch().Name;
                var a5 = configComboBox.SelectedIndex;
                var backgroundWorker = new BackgroundWorkerQueueDialog("Loading servers...", ProgressBarStyle.Marquee);
                backgroundWorker.AppendTask("Connecting machines...", DummyWorker.GetWorker((ctx) => {
                    try
                    {
                        Interface.NewTemplate(a1, a2, a3, a4, jsonHelper.JObject["templates"][a5]);
                        ctx.s = StatusCode.OK;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK);
                        ctx.s = StatusCode.FAILED;
                    }
                    return ctx;
                }));
                backgroundWorker.ShowDialog();
                if (backgroundWorker.GetWorker().ReturnedObjects[0].s == StatusCode.OK)
                    this.DialogResult = DialogResult.OK;
            }
        }

        private void AddTemplateDialog_Load(object sender, EventArgs e)
        {
            // Get server information
            BackgroundWorkerQueueDialog backgroundWorker = new BackgroundWorkerQueueDialog("Retrieving Network Information");
            foreach (var host in SessionManager.GetDatabase().GetServerDb())
            {
                backgroundWorker.AppendTask("Validating " + host.HostName, NetworkWorkers.GetStarterWorker(
                    new NetworkWorkerObject { HostName = host.HostName }
                ));
                backgroundWorker.AppendTask("Getting IP address of " + host.HostName + "...", NetworkWorkers.GetIpWorker());
            }
            backgroundWorker.ShowDialog();
            var bw = backgroundWorker.GetWorker();
            for (int i = 0; i < SessionManager.GetDatabase().GetServerDb().Count; i++)
            {
                if (bw.ReturnedObjects[i * 2 + 1].s == StatusCode.OK)
                {
                    hostComputers.Add(new HostComputer
                    {
                        HostName = ((NetworkWorkerObject)bw.ReturnedObjects[i * 2 + 1].o).HostName,
                        IpAddress = ((NetworkWorkerObject)bw.ReturnedObjects[i * 2 + 1].o).IpAddress,
                    });
                }
            }

            // Get configurations
            for (int i = 0; i < jsonHelper.JObject["templates"]?.Count(); i++)
                configComboBox.ComboBox.Items.Add(jsonHelper.JObject["templates"][i]["name"]);

            // Add options to host combobox
            hostComputers.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x.HostName) && !string.IsNullOrEmpty(x.IpAddress))
                    serverComboBox.ComboBox.Items.Add(x.HostName + " [" + x.IpAddress + "]");
            });

            // Get virtual machines
            backgroundWorker = new BackgroundWorkerQueueDialog("Scanning for Virtual Machines...", ProgressBarStyle.Marquee);
            backgroundWorker.AppendTask("Getting machines...", DummyWorker.GetWorker(() =>
            {
                SessionManager.GetDatabase().FlushCache();
                try
                {
                    foreach (var vm in SessionManager.GetDatabase().GetVm(VirtualMachineType.BASE))
                        virtualMachines.Add(vm);
                    virtualMachines.ForEach(x =>
                    {
                        ThreadManager.Invoke(this, baseComboBox, () => 
                            baseComboBox.ComboBox.Items.Add(x.Name + " [" + x.Host + "]")
                        );
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK);
                }
            }));
            backgroundWorker.ShowDialog();
        }

        private void AddTemplateDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            jsonHelper.Dispose();
        }

        #endregion
    }
}
