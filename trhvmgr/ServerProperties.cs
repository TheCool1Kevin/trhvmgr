using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
    public class ServerPropertyGrid
    {
        [DisplayName("DNS Name"), Category("General Information"), ReadOnly(true)]
        string HostName { get; set; }

        [DisplayName("VHD Path"), DefaultValue(@"C:\VHDs\"), Category("General Information"), ReadOnly(true)]
        string VirtualHardDiskPath { get; set; }

        [DisplayName("VM Path"), DefaultValue(@"C:\ProgramData\Microsoft\Windows\Hyper-V"), Category("General Information"), ReadOnly(true)]
        string VirtualMachinePath { get; set; }

        [DisplayName("MAC Address Maximum"), Category("Network"), ReadOnly(false)]
        string MacAddressMaximum { get; set; }

        [DisplayName("MAC Address Minimum"), Category("Network"), ReadOnly(false)]
        string MacAddressMinimum { get; set; }

        [DisplayName("Enable Enhanced Sessions"), Category("User"), ReadOnly(false)]
        bool EnableEnhancedSessionMode { get; set; }

        [DisplayName("Installed Memory"), Category("General Information"), TypeConverter(typeof(CustomNumberTypeConverter)), ReadOnly(true)]
        long MemoryCapacity { get; set; }

        [DisplayName("Logical Processors"), Category("General Information"), ReadOnly(true)]
        int LogicalProcessorCount { get; set; }

        public ServerPropertyGrid(string hostName)
        {
            this.HostName = hostName;
            // Populate all
            PSObject psObj = null;
            new BackgroundWorkerQueueDialog("Getting server information...", ProgressBarStyle.Marquee)
                .AppendTask("", DummyWorker.GetWorker((ctx) => psObj = HyperV.GetVmHost(hostName, PsStreamEventHandlers.GetUIHandlers(ctx))))
                .ShowDialog();
            if (psObj == null) return;

            this.VirtualHardDiskPath = (string)psObj.Members["VirtualHardDiskPath"].Value;
            this.VirtualMachinePath = (string)psObj.Members["VirtualMachinePath"].Value;
            this.MacAddressMaximum = (string)psObj.Members["MacAddressMaximum"].Value;
            this.MacAddressMinimum = (string)psObj.Members["MacAddressMinimum"].Value;
            this.EnableEnhancedSessionMode = (bool)psObj.Members["EnableEnhancedSessionMode"].Value;
            this.MemoryCapacity = (long)psObj.Members["MemoryCapacity"].Value;
            this.LogicalProcessorCount = (int)psObj.Members["LogicalProcessorCount"].Value;
        }
    }

    public class CustomNumberTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context,
                                            Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context,
            CultureInfo culture, object value)
        {
            if (value is string)
            {
                string s = (string)value;
                return long.Parse(s, NumberStyles.AllowThousands, culture);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context,
            CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
                return ServerProperties.FormatBytes((long)value);
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    public partial class ServerProperties : Form
    {
        public static string FormatBytes(long bytes)
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

        public ServerPropertyGrid gridObj;

        public ServerProperties(string hostName)
        {
            gridObj = new ServerPropertyGrid(hostName);
            InitializeComponent();
        }
    }
}
