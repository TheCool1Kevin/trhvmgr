using System;
using System.ComponentModel;
using System.Windows.Forms;
using trhvmgr.Properties;
using trhvmgr.UI;

namespace trhvmgr
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Initialize session
            SessionManager.Instance.InitializeDatabase();
            SettingsAttribute.SetAttribute("Setting",
                new DisplayNameAttribute("Some text"),
                new DescriptionAttribute("This is a description"),
                new DefaultValueAttribute(Settings.Default.Properties["Setting"].DefaultValue));

            // Initialize application
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainFrm());
        }
    }
}
