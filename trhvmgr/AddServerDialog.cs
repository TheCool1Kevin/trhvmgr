using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using trhvmgr.Lib;
using trhvmgr.Objects;
using trhvmgr.Plugs;
using trhvmgr.UI;

namespace trhvmgr
{
    public partial class AddServerDialog : Form
    {
        public AddServerDialog()
        {
            InitializeComponent();
            // Install event handlers this way (it's easier)
            ipText.ButtonClick += (x, y) => { ValidateTexts(); };
            macText.ButtonClick += (x, y) => { ValidateTexts(); };
        }

        #region Private Methods

        private void ValidateTexts()
        {
            // A clean alternative way to run consecutive network events
            BackgroundWorkerQueueDialog backgroundWorker = new BackgroundWorkerQueueDialog("Retrieving Network Information");
            backgroundWorker.AppendTask("Starting...", NetworkWorkers.GetStarterWorker(new NetworkWorkerObject
            {
                HostName = hostnameText.Text,
                IpAddress = ipText.Text,
                MacAddress = macText.Text
            }));
            backgroundWorker.AppendTask("Getting IP address from server...", NetworkWorkers.GetIpWorker());
            backgroundWorker.AppendTask("Getting MAC address from IP...", NetworkWorkers.GetMacWorker());
            backgroundWorker.ShowDialog();
            var v = backgroundWorker.GetWorker();

            // Set fields, only if the returned object has text - therefore preserving whatever was typed beforehand
            if (backgroundWorker.GetWorker().ReturnedObjects[1].s == (int)StatusCode.OK)
                ipText.Text = ((NetworkWorkerObject)backgroundWorker.GetWorker().ReturnedObjects[1].o).IpAddress;
            if (backgroundWorker.GetWorker().ReturnedObjects[2].s == (int)StatusCode.OK)
                macText.Text = ((NetworkWorkerObject)backgroundWorker.GetWorker().ReturnedObjects[2].o).MacAddress;

            // If something's empty/failed, let user know
            hostnameText.IsValid = (string.IsNullOrWhiteSpace(hostnameText.Text)) ?
                    tribool.FALSE : tribool.TRUE;
            ipText.IsValid = (string.IsNullOrWhiteSpace(ipText.Text) ||
                backgroundWorker.GetWorker().ReturnedObjects[1].s != (int)StatusCode.OK) ?
                    tribool.FALSE : tribool.TRUE;
            macText.IsValid = (string.IsNullOrWhiteSpace(macText.Text) ||
                backgroundWorker.GetWorker().ReturnedObjects[2].s != (int)StatusCode.OK) ?
                    tribool.FALSE : tribool.TRUE;

            // Get VMs
            listBox1.Items.Clear();
            backgroundWorker = new BackgroundWorkerQueueDialog("Scanning for Virtual Machines...");
            backgroundWorker.AppendTask("Getting machines...", DummyWorker.GetWorker(() =>
            {
                try
                {
                    Interface.GetVms(hostnameText.Text)?.ForEach(x =>
                    {
                        ThreadManager.Invoke(this, listBox1, () =>
                            listBox1.Items.Add(x.Name + " [" + x.Uuid.ToString().ToUpper() + "]"));
                    });
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Exception", MessageBoxButtons.OK);
                }
            }));
            backgroundWorker.ShowDialog();
        }

        #endregion

        #region UI Events

        private void addbtn_Click(object sender, EventArgs e)
        {
            ValidateTexts();
            if(hostnameText.IsValid == tribool.TRUE && ipText.IsValid == tribool.TRUE /*&& macText.IsValid == tribool.TRUE*/)
            {
                this.DialogResult = DialogResult.OK;
                SessionManager.Instance.Database.AddServer(new DbHostComputer { HostName = hostnameText.Text });
            }
            else
                this.DialogResult = DialogResult.None;
        }

        private void checkBox1_MouseHover(object sender, EventArgs e)
        {
            this.toolTip1.Show(
                "This prevents the checking mechanisms from overwriting what was\n" +
                "typed in the textboxes. It is strongly advised to leave this\n"+
                "unchecked, unless there are issues when adding the server.\n" +
                "Subsequent execution of the program MAY fail."
            , this.checkBox1);
        }

        private void checkBox1_MouseLeave(object sender, EventArgs e)
        {
            this.toolTip1.Hide(this.checkBox1);
        }

        #endregion
    }
}
