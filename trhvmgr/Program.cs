using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
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
            SettingsAttribute.SetAttribute(
                "templateFile", "Configuration Files", "Template file path",
                "Path to the JSON configuration file containing all VM templates",
                new EditorAttribute(typeof(JsonFileSelectorTypeEditor), typeof(UITypeEditor))
            );

            // Initialize application
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainFrm());
        }
    }
}
