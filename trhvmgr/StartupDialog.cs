using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trhvmgr
{
    public partial class StartupDialog : Form
    {
        public StartupDialog()
        {
            InitializeComponent();
            dbfileTxt.Text = ConfigurationManager.ConnectionStrings["ServerDB"].ConnectionString;
            serverTxt.Text = Dns.GetHostName();
        }

        #region UI Events

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            // Launch console application
            if(checkBox1.Checked)
            {
                Process.Start("trhvmgr.Interactive.exe");
                DialogResult = DialogResult.Abort;
            }
            else
            {
                DialogResult = DialogResult.OK;
                return;
            }

            DialogResult = DialogResult.None;
        }

        #endregion
    }
}
