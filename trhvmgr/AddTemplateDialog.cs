using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using trhvmgr.Lib;
using trhvmgr.Objects;
using trhvmgr.Plugs;
using trhvmgr.UI;

namespace trhvmgr
{
    public partial class AddTemplateDialog : Form
    {
        private List<HostComputer> hostComputers = new List<HostComputer>();
        private List<VirtualMachine> virtualMachines = new List<VirtualMachine>();

        public AddTemplateDialog()
        {
            InitializeComponent();
        }

        #region UI Events

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;      // WS_EX_COMPOSITED
                return handleParam;
            }
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            bool isFormValid = false;
            if (serverComboBox.SelectedIndex == -1)
                serverComboBox.IsValid = tribool.FALSE;
            if (baseComboBox.SelectedIndex == -1)
                baseComboBox.IsValid = tribool.FALSE;
            if (string.IsNullOrEmpty(vmTextbox.Text))
                vmTextbox.IsValid = tribool.FALSE;

            isFormValid = serverComboBox.IsValid == tribool.TRUE && baseComboBox.IsValid == tribool.TRUE && vmTextbox.IsValid == tribool.TRUE;

            if (!isFormValid) this.DialogResult = DialogResult.None;
            else
            {

            }
        }

        private void AddTemplateDialog_Load(object sender, EventArgs e)
        {
            // Get server information
            BackgroundWorkerQueueDialog backgroundWorker = new BackgroundWorkerQueueDialog("Retrieving Network Information");
            foreach (var host in SessionManager.GetDatabase().GetServers())
            {
                backgroundWorker.AppendTask("Validating " + host.HostName, NetworkWorkers.GetStarterWorker(
                    new NetworkWorkerObject { HostName = host.HostName }
                ));
                backgroundWorker.AppendTask("Getting IP address of " + host.HostName + "...", NetworkWorkers.GetIpWorker());
                backgroundWorker.AppendTask("Getting MAC address of " + host.HostName + "...", NetworkWorkers.GetMacWorker());
            }
            backgroundWorker.ShowDialog();
            var bw = backgroundWorker.GetWorker();
            for (int i = 0; i < SessionManager.GetDatabase().GetServers().Count; i++)
            {
                if (bw.ReturnedObjects[i * 3 + 2].s == StatusCode.OK)
                {
                    hostComputers.Add(new HostComputer
                    {
                        HostName = ((NetworkWorkerObject)bw.ReturnedObjects[i * 3 + 2].o).HostName,
                        IpAddress = ((NetworkWorkerObject)bw.ReturnedObjects[i * 3 + 2].o).IpAddress,
                        MacAddress = ((NetworkWorkerObject)bw.ReturnedObjects[i * 3 + 2].o).MacAddress
                    });
                }
            }

            // Add options to combobox
            serverComboBox.ComboBox.Items.Add("[Automatic]");
            hostComputers.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x.HostName) && !string.IsNullOrEmpty(x.IpAddress) && !string.IsNullOrEmpty(x.MacAddress))
                    serverComboBox.ComboBox.Items.Add(x.HostName + " [" + x.IpAddress + "]");
            });

            // Get virtual machines
            backgroundWorker = new BackgroundWorkerQueueDialog("Scanning for Virtual Machines...", ProgressBarStyle.Marquee);
            backgroundWorker.AppendTask("Getting machines...", DummyWorker.GetWorker(() =>
            {
                SessionManager.GetDatabase().FlushCache();
                try
                {
                    foreach (var vm in SessionManager.GetDatabase().GetVms(VirtualMachineType.BASE))
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

        #endregion
    }
}
