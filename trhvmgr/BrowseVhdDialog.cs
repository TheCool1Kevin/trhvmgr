using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using trhvmgr.Lib;
using trhvmgr.Objects;
using trhvmgr.UI;

namespace trhvmgr
{
    public partial class BrowseVhdDialog : Form
    {
        private List<HostComputer> hostComputers = new List<HostComputer>();

        public string FileName { get; set; }
        public string ComputerName { get; set; }

        public BrowseVhdDialog()
        {
            InitializeComponent();
        }

        private HostComputer GetSelectedHost()
        {
            if (serverComboBox.ComboBox.SelectedIndex >= 0 && serverComboBox.ComboBox.SelectedIndex < hostComputers.Count())
                return hostComputers[serverComboBox.ComboBox.SelectedIndex];
            return null;
        }

        private void BrowseVhdDialog_Load(object sender, EventArgs e)
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
            // Add options to host combobox
            hostComputers.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x.HostName) && !string.IsNullOrEmpty(x.IpAddress))
                    serverComboBox.ComboBox.Items.Add(x.HostName + " [" + x.IpAddress + "]");
            });

            serverComboBox.ComboBox.SelectedIndexChanged += (o, ev) =>
            {
                if (Dns.GetHostName() != GetSelectedHost().HostName)
                    button3.Enabled = false;
                else
                    button3.Enabled = true;
            };
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Dns.GetHostName() != GetSelectedHost().HostName)
            {
                button3.Enabled = false;
                return;
            }
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Multiselect = false,
                Title = "Choose a virtual hard disk...",
                Filter = "Virtual hard disks (*.vhdx;*.vhd)|*.vhdx;*.vhd|Checkpoint files (*.avhd;*.avhdx)|*.avhd;*.avhdx|All Files (*.*)|*.*",
                InitialDirectory = "C:\\VHDs"
            };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.ComputerName = GetSelectedHost().HostName;
                this.FileName = fileDialog.FileName;
                this.validatingTextbox1.Text = this.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (serverComboBox.SelectedIndex == -1) serverComboBox.IsValid = tribool.FALSE; else serverComboBox.IsValid = tribool.TRUE;
            if (string.IsNullOrEmpty(validatingTextbox1.Text)) validatingTextbox1.IsValid = tribool.FALSE; else validatingTextbox1.IsValid = tribool.TRUE;
            bool isFormValid = serverComboBox.IsValid == tribool.TRUE && validatingTextbox1.IsValid == tribool.TRUE;
            if(isFormValid)
            {
                this.ComputerName = GetSelectedHost().HostName;
                this.FileName = validatingTextbox1.Text;
                DialogResult = DialogResult.OK;
                return;
            }

            DialogResult = DialogResult.None;
        }
    }
}
