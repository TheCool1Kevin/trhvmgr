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
        // Alias: Get-VM
        public static List<PSObject> GetVm(string ComputerName)
        {
            List<PSObject> vms = new List<PSObject>();
            Runspace runspace = Interface.GetRunspace(ComputerName);
            runspace.Open();
            using (PowerShell ps = PowerShell.Create())
            {
                ps.Runspace = runspace;
                ps.AddStatement().AddCommand("Get-VM");
                var results = ps.Invoke();
                vms.AddRange(results);
            }
            return vms;
        }
    }
}
