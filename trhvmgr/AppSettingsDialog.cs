using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trhvmgr
{
    public partial class AppSettingsDialog : Form
    {
        public AppSettingsDialog()
        {
            InitializeComponent();
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This action is irreversible. Do you want to continue?", "Reset to Default", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                Properties.Settings.Default.Reset();
            propertyGrid.Refresh();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            propertyGrid.Refresh();
        }
    }
}
