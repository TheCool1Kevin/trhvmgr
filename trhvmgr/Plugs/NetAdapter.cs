using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using trhvmgr.Lib;
using trhvmgr.Plugs;

namespace trhvmgr.Plugs
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
            res.AddRange(PSWrapper.Execute(ComputerName, (ps) =>
                ps.AddStatement().AddCommand("Get-NetAdapter").AddParameter("Physical").Invoke()));
            return res;
        }
    }
}
