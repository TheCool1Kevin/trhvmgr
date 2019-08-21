using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using trhvmgr.Lib;

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

        

        #endregion

        #region Get

        // Alias: Get-VM
        public static List<PSObject> GetVm(string ComputerName)
        {
            Collection<PSObject> res;
            PSWrapper.Execute(ComputerName, "Get-VM", out res);
            return res.ToList();
        }

        // Alias: Get-VMSwitch
        public static List<PSObject> GetVmSwitch(string ComputerName)
        {
            Collection<PSObject> res;
            PSWrapper.Execute(ComputerName, "Get-VMSwitch", out res);
            return res.ToList();
        }

        #endregion

        #region New

        // Alias: New-VM
        public static PSObject NewVm(string ComputerName, Dictionary<string, object> Parameters)
        {
            return PSWrapper.Execute(ComputerName, (ps) =>
            {
                ps.AddStatement().AddCommand("New-VM")
                    .AddParameters(Parameters)
                    .AddParameter("Force");
                return ps.Invoke();
            })[0];
        }

        #endregion

        #region Set


        
        #endregion
    }
}
