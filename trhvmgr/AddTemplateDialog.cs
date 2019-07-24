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

        }

        private void AddTemplateDialog_Load(object sender, EventArgs e)
        {
            // Get server information
            BackgroundWorkerQueueDialog backgroundWorker = new BackgroundWorkerQueueDialog("Retrieving Network Information");
            foreach (var host in SessionManager.Instance.Database.GetServers())
            {
                backgroundWorker.AppendTask("Validating " + host.HostName, NetworkWorkers.GetStarterWorker(
                    new NetworkWorkerObject { HostName = host.HostName }
                ));
                backgroundWorker.AppendTask("Getting IP address of " + host.HostName + "...", NetworkWorkers.GetIpWorker());
                backgroundWorker.AppendTask("Getting MAC address of " + host.HostName + "...", NetworkWorkers.GetMacWorker());
            }
            backgroundWorker.ShowDialog();
            var bw = backgroundWorker.GetWorker();
            for (int i = 0; i < SessionManager.Instance.Database.GetServers().Count; i++)
            {
                if (bw.ReturnedObjects[i * 3 + 2].s == (int)StatusCode.OK)
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
            foreach (var s in hostComputers)
                foreach (var vm in Interface.GetVms(s.HostName))
                    if (vm.Type == VirtualMachineType.BASE)
                        virtualMachines.Add(vm);
            virtualMachines.ForEach(x =>
            {
                baseComboBox.ComboBox.Items.Add(x.Name + " [" + x.Host + "]");
            });

            // Make all default selected indicies 0
            //serverComboBox.SelectedIndex = 0;
            //baseComboBox.SelectedIndex = 0;
        }

        #endregion
    }
}
