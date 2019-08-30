using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using trhvmgr.Lib;
using trhvmgr.Objects;

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
        public static List<PSObject> GetPsVm(string ComputerName)
        {
            Collection<PSObject> res;
            PSWrapper.Execute(ComputerName, "Get-VM", out res);
            return res.ToList();
        }

        // Alias: Get-VM
        /// <exception cref="Exception">May throw exceptions</exception>
        public static List<VirtualMachine> GetVm(string hostName, PsStreamEventHandlers handlers = null)
        {
            Collection<PSObject> objs = null;
            PSWrapper.FastExecute(hostName, "Get-Vm", out objs, handlers);
            if (objs == null || objs.Count == 0) return new List<VirtualMachine>();
            List<VirtualMachine> machines = new List<VirtualMachine>();
            foreach (var m in objs)
                machines.Add(VirtualMachine.FromPSObject(m, hostName));
            return machines;
        }

        // Alias: Get-VM
        /// <exception cref="Exception">May throw exceptions</exception>
        public static VirtualMachine GetVm(string hostName, string Name, PsStreamEventHandlers handlers = null)
        {
            Collection<PSObject> objs = null;
            PSWrapper.FastExecute(hostName, $"Get-Vm -Name \"{Name}\"", out objs, handlers);
            if (objs == null || objs.Count == 0) return null;
            return VirtualMachine.FromPSObject(objs[0], hostName);
        }

        // Alias: Get-VMSwitch
        public static List<PSObject> GetVmSwitch(string ComputerName)
        {
            Collection<PSObject> res;
            PSWrapper.FastExecute(ComputerName, "Get-VMSwitch", out res);
            return res.ToList();
        }

        // Alias: Get-VHD
        public static List<PSObject> GetVhd(string ComputerName, string Path, PsStreamEventHandlers handlers = null)
        {
            Collection<PSObject> res;
            res = PSWrapper.FastExecute(ComputerName, (ps) =>
            {
                return ps.AddCommand("Get-VHD").AddParameter("Path", Path).Invoke();
            }, handlers);
            return res.ToList();
        }

        // Alias: Get-VMHost
        public static PSObject GetVmHost(string ComputerName, PsStreamEventHandlers handlers = null)
        {
            Collection<PSObject> res;
            PSWrapper.Execute(ComputerName, $"Get-VMHost -ComputerName \"{ComputerName}\"", out res, handlers);
            if (res == null || res.Count <= 0) return null;
            return res[0];
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

        // Alias: New-VHD
        public static PSObject NewVHD(string ComputerName, string ParentPath, string Path, PsStreamEventHandlers handlers = null)
        {
            return PSWrapper.Execute(ComputerName, (ps) =>
            {
                ps.AddStatement().AddScript($"New-VHD -ParentPath \"{ParentPath}\" -Path \"{Path}\" -Differencing");
                return ps.Invoke();
            }, handlers)[0];
        }

        #endregion

        #region Set

        // Alias: Set-VHD
        public static void SetVHD(string ComputerName, string ParentPath, string Path, bool IgnoreMismatchId, PsStreamEventHandlers handlers = null)
        {
            PSWrapper.Execute(ComputerName, (ps) =>
            {
                string Flag = /*IgnoreMismatchId ? "-IgnoreMismatchId" :*/ "";
                ps.AddStatement().AddScript($"Set-VHD -Path \"{Path}\" -ParentPath \"{ParentPath}\" {Flag}");
                return ps.Invoke();
            }, handlers);
        }

        #endregion

        #region Misc

        // Alias: Remove-VM
        public static void RemoveVm(string ComputerName, string Name, PsStreamEventHandlers handlers = null)
        {
            Collection<PSObject> res;
            PSWrapper.Execute(ComputerName, $"Remove-VM -Name \"{Name}\" -Force", out res, handlers);
        }

        // Alias: Restore-VMSnapshot
        public static void RestoreVmSnapshot(string ComputerName, string VMName, string Name, PsStreamEventHandlers handlers = null)
        {
            Collection<PSObject> res;
            PSWrapper.Execute(ComputerName, $"Restore-VMSnapshot -Name \"{Name}\" -VMName \"{VMName}\" -Confirm:$false", out res, handlers);
        }

        // Alias: Checkpoint-VM
        public static void CheckpointVm(string ComputerName, string VMName, string Name, PsStreamEventHandlers handlers = null)
        {
            Collection<PSObject> res;
            PSWrapper.Execute(ComputerName, $"Checkpoint-VM -SnapshotName \"{Name}\" -Name \"{VMName}\" -Confirm:$false", out res, handlers);
        }

        // Alias: Remove-VMSnapshot
        public static void RemoveVmSnapshot(string ComputerName, string VMName, string Name, PsStreamEventHandlers handlers = null)
        {
            Collection<PSObject> res;
            PSWrapper.Execute(ComputerName, $"Remove-VMSnapshot -Name \"{Name}\" -VMName \"{VMName}\" -IncludeAllChildSnapshots -Confirm:$false", out res, handlers);
        }

        // Alias: Start-VM
        public static void StartVm(string ComputerName, string Name, PsStreamEventHandlers handlers = null)
        {
            Collection<PSObject> res;
            PSWrapper.Execute(ComputerName, $"Start-VM -Name \"{Name}\"", out res, handlers);
        }

        // Alias: Stop-VM
        public static void StopVm(string ComputerName, string Name, bool PowerOff, PsStreamEventHandlers handlers = null)
        {
            Collection<PSObject> res;
            string Flag = PowerOff ? "-PowerOff" : "";
            PSWrapper.Execute(ComputerName, $"Stop-VM -Name \"{Name}\" {Flag} -Force", out res, handlers);
        }

        // Alias: Resume-VM
        public static void ResumeVm(string ComputerName, string Name, PsStreamEventHandlers handlers = null)
        {
            Collection<PSObject> res;
            PSWrapper.Execute(ComputerName, $"Resume-VM -Name \"{Name}\"", out res, handlers);
        }

        // Alias: Suspend-VM
        public static void SuspendVm(string ComputerName, string Name, PsStreamEventHandlers handlers = null)
        {
            Collection<PSObject> res;
            PSWrapper.Execute(ComputerName, $"Suspend-VM -Name \"{Name}\"", out res, handlers);
        }

        // Alias: Save-VM
        public static void SaveVm(string ComputerName, string Name, PsStreamEventHandlers handlers = null)
        {
            Collection<PSObject> res;
            PSWrapper.Execute(ComputerName, $"Save-VM -Name \"{Name}\"", out res, handlers);
        }

        #endregion
    }
}
