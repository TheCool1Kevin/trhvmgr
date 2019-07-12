using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;

namespace trhvmgr.Plugs
{
    /// <summary>
    /// Interface for Hyper-V Powershell Module,
    /// as documented here:
    /// https://docs.microsoft.com/en-us/powershell/module/hyper-v/
    /// </summary>
    public class HyperV
    {
        #region Add

        // Alias: Add-VMNetworkAdapter
        public static bool AddVMNetworkAdapter(string ComputerName, string Id, /*string Name,*/ string SwitchName, bool IsLegacy)
        {
            Runspace runspace = Interface.GetRunspace(ComputerName);
            runspace.Open();
            using (PowerShell ps = PowerShell.Create())
            {
                ps.Runspace = runspace;
                ps.AddStatement().AddCommand("Get-VM")
                    .AddParameter("Id", Id)
                    .AddCommand("Add-VMNetworkAdapter")
                    .AddParameter("IsLegacy", IsLegacy)
                    //.AddParameter("Name", Name)
                    .AddParameter("SwitchName", SwitchName);
                ps.Invoke();
                if (ps.HadErrors) return false;
            }
            return true;
        }

        #endregion

        #region Get

        // Alias: Get-VM
        public static List<PSObject> GetVm(string ComputerName)
        {
            List<PSObject> res = new List<PSObject>();
            Runspace runspace = Interface.GetRunspace(ComputerName);
            runspace.Open();
            using (PowerShell ps = PowerShell.Create())
            {
                ps.Runspace = runspace;
                ps.AddStatement().AddCommand("Get-VM");
                var results = ps.Invoke();
                res.AddRange(results);
            }
            return res;
        }

        #endregion

        #region New

        // Alias: New-VM
        public static PSObject NewVm(string ComputerName, Dictionary<string, object> Parameters)
        {
            PSObject res = null;
            Runspace runspace = Interface.GetRunspace(ComputerName);
            runspace.Open();
            using (PowerShell ps = PowerShell.Create())
            {
                ps.Runspace = runspace;
                ps.AddStatement().AddCommand("New-VM")
                    .AddParameters(Parameters)
                    .AddParameter("Force");
                res = ps.Invoke()[0];
            }

            return res;
        }

        #endregion

        #region Set

        // Alias: Set-VM
        public static bool SetVm(string ComputerName, Dictionary<string, object> Parameters)
        {
            Runspace runspace = Interface.GetRunspace(ComputerName);
            runspace.Open();
            using (PowerShell ps = PowerShell.Create())
            {
                ps.Runspace = runspace;
                ps.AddStatement()
                    .AddCommand("Set-VMMemory")
                    .AddParameters(Parameters);
                ps.Invoke();
                if (ps.HadErrors) return false;
            }
            return true;
        }

        // Alias: Set-VMBios
        public static bool SetVmBios(string ComputerName, Dictionary<string, object> Parameters)
        {
            Runspace runspace = Interface.GetRunspace(ComputerName);
            runspace.Open();
            using (PowerShell ps = PowerShell.Create())
            {
                ps.Runspace = runspace;
                ps.AddStatement()
                    .AddCommand("Set-VMBios")
                    .AddParameters(Parameters);
                ps.Invoke();
                if (ps.HadErrors) return false;
            }
            return true;
        }

        // Alias: Get-VMSwitch
        public static List<PSObject> GetVmSwitch(string ComputerName)
        {
            List<PSObject> res = new List<PSObject>();
            Runspace runspace = Interface.GetRunspace(ComputerName);
            runspace.Open();
            using (PowerShell ps = PowerShell.Create())
            {
                ps.Runspace = runspace;
                ps.AddStatement().AddCommand("Get-VMSwitch");
                var results = ps.Invoke();
                res.AddRange(results);
            }
            return res;
        }

        #endregion
    }
}
