using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Reflection;
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
                if (checkBox2.Checked)
                {
                    new CredentialManagement.Credential
                    {
                        Target = (Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0] as AssemblyTitleAttribute).Title,
                        Username = userTxt.Text,
                        Password = pswTxt.Text,
                        PersistanceType = CredentialManagement.PersistanceType.LocalComputer
                    }.Save();
                }
                DialogResult = DialogResult.OK;
                PSCredential cred = new PSCredential(userTxt.Text, new NetworkCredential("", pswTxt.Text).SecurePassword);
                SessionManager.Instance.SetCredential(cred);
                return;
            }

            DialogResult = DialogResult.None;
        }

        private void StartupDialog_Load(object sender, EventArgs e)
        {
            pswTxt.textBox1.TextBox.UseSystemPasswordChar = true;
            var cred = new CredentialManagement.Credential
            {
                Target = (Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0] as AssemblyTitleAttribute).Title
            };
            if (cred.Load())
            {
                userTxt.Text = cred.Username;
                pswTxt.Text = cred.Password;
            }
        }

        private void StartupDialog_Shown(object sender, EventArgs e)
        {
            userTxt.Focus();
        }

        #endregion
    }
}
