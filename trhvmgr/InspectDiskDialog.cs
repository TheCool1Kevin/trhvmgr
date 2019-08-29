using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using trhvmgr.Lib;
using trhvmgr.Plugs;
using trhvmgr.UI;

namespace trhvmgr
{
    public partial class InspectDiskDialog : Form
    {
        private static string FormatBytes(long bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return string.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }

        private PSObject pso = null;
        private string c = "";

        public InspectDiskDialog(string computer, string path)
        {
            InitializeComponent();
            var bg = new BackgroundWorkerQueueDialog("Getting information...", ProgressBarStyle.Marquee);
            bg.AppendTask("Getting information...", DummyWorker.GetWorker((ctx) =>
            {
                try
                {
                    pso = HyperV.GetVhd(computer, path, PsStreamEventHandlers.GetUIHandlers(ctx))[0];
                    ctx.s = StatusCode.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK);
                    ctx.s = StatusCode.FAILED;
                }
                return ctx;
            }));
            bg.ShowDialog();
            this.c = computer;
        }

        private void InspectDiskDialog_Load(object sender, EventArgs e)
        {
            textBox1.Text = (string)pso?.Members["VhdFormat"].Value;
            textBox2.Text = (string)pso?.Members["VhdType"].Value;
            textBox3.Text = Path.GetDirectoryName((string)pso?.Members["Path"].Value);
            textBox4.Text = Path.GetFileName((string)pso?.Members["Path"].Value);
            textBox5.Text = FormatBytes(long.Parse(pso?.Members["FileSize"].Value.ToString()));
            textBox6.Text = FormatBytes(long.Parse(pso?.Members["Size"].Value.ToString())); 
            textBox7.Text = (string)pso?.Members["ParentPath"].Value;

            if(!string.IsNullOrEmpty((string)pso?.Members["ParentPath"].Value))
                button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new InspectDiskDialog(c, textBox7.Text).ShowDialog();
        }
    }
}
