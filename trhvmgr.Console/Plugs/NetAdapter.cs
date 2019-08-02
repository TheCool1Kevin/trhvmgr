using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using trhvmgr.Plugs;

namespace trhvmgr.Interactive.Plugs
{
    /// <summary>
    /// Interface for netadapter Powershell Module,
    /// as documented here:
    /// https://docs.microsoft.com/en-us/powershell/module/netadapter/
    /// </summary>
    public class NetAdapter
    {
        // Alias: Get-NetAdapter -Physical
        public static List<PSObject> GetNetAdapter(string ComputerName)
        {
            List<PSObject> res = new List<PSObject>();
            Runspace runspace = Interface.GetRunspace(ComputerName);
            runspace.Open();
            using (PowerShell ps = PowerShell.Create())
            {
                ps.Runspace = runspace;
                ps.AddStatement().AddCommand("Get-NetAdapter").AddParameter("Physical");
                var results = ps.Invoke();
                res.AddRange(results);
            }
            return res;
        }
    }
}
