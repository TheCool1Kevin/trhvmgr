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
    public class Interface
    {
        #region Powershell Credentials

        public static Runspace GetRunspace(PSCredential cred, string hostName)
        {
            WSManConnectionInfo ci = new WSManConnectionInfo();
            ci.ComputerName = hostName;
            ci.AuthenticationMechanism = AuthenticationMechanism.Kerberos;
            ci.Credential = cred;
            Runspace runspace = RunspaceFactory.CreateRunspace(ci);
            return runspace;
        }

        public static Runspace GetRunspace(string hostName)
        {
            return GetRunspace(SessionManager.Instance.PSCredential, hostName);
        }

        public static void BringOnline(string hostName, PsStreamEventHandlers handlers = null)
        {
            using (PowerShell ps = PowerShell.Create())
            {
                PsStreamEventHandlers.RegisterHandlers(ps, handlers);
                var ret = ps.AddCommand("New-PSSession").AddParameter("ComputerName", hostName).AddParameter("Credential", SessionManager.Instance.PSCredential).Invoke();
                ps.AddCommand("Remove-PSSession").AddParameter("Session", ret).Invoke();
            }
        }

        public static void RestartService(string hostName, PsStreamEventHandlers handlers = null)
        {
            PSWrapper.Execute(hostName, (ps) => ps.AddCommand("Restart-Service").AddParameter("Name", "vmms").Invoke(), handlers);
        }

        #endregion

        #region Virtual Machine State Query

        public static List<VirtualMachine> GetVms(string hostName)
        {
            Collection<PSObject> objs = null;
            PSWrapper.Execute(hostName, "Get-Vm", out objs);
            if (objs == null || objs.Count == 0) return null;

            List<VirtualMachine> machines = new List<VirtualMachine>();
            foreach(var m in objs)
            {
                machines.Add(new VirtualMachine {
                    Host = hostName,
                    Name = m.Members["VMName"].Value.ToString(),
                    Uuid = (Guid)m.Members["VMId"].Value,
                    VhdPath = Array.ConvertAll(PSWrapper.Execute(hostName, (ps) =>
                    {
                        return ps.AddCommand("Get-VM")
                            .AddParameter("Id", m.Members["VMId"].Value)
                            .AddCommand("Get-VMHardDiskDrive")
                            .Invoke();
                    }).ToArray(), (x) => { return x.Members["Path"].Value.ToString(); }),
                    Type = VirtualMachineType.BASE
                });
            }
            return machines;
        }

        #endregion
    }
}
