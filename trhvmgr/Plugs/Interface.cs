using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using trhvmgr.Lib;
using trhvmgr.Objects;
using trhvmgr.Properties;

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
            return GetRunspace(SessionManager.GetCredential(), hostName);
        }

        /// <exception cref="Exception">May throw exceptions</exception>
        public static void BringOnline(string hostName, PsStreamEventHandlers handlers = null)
        {
            using (PowerShell ps = PowerShell.Create())
            {
                PsStreamEventHandlers.RegisterHandlers(ps, handlers);
                var ret = ps.AddCommand("New-PSSession").AddParameter("ComputerName", hostName).AddParameter("Credential", SessionManager.GetCredential()).Invoke();
                ps.AddCommand("Remove-PSSession").AddParameter("Session", ret).Invoke();
            }
        }

        /// <exception cref="Exception">May throw exceptions</exception>
        public static void RestartService(string hostName, PsStreamEventHandlers handlers = null)
        {
            PSWrapper.Execute(hostName, (ps) => ps.AddCommand("Restart-Service").AddParameter("Name", "vmms").Invoke(), handlers);
        }

        #endregion

        #region Powershell Utilities

        public static void CopyFile(string srcHost, string dstHost, string src, string dst)
        {

        }
        
        #endregion

        #region Virtual Machine State Query

        /// <exception cref="Exception">May throw exceptions</exception>
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
                    State = VirtualMachine.GetStateFromString(m.Members["State"].Value.ToString()),
                    VhdPath = Array.ConvertAll(PSWrapper.Execute(hostName, (ps) =>
                    {
                        return ps.AddCommand("Get-VM")
                            .AddParameter("Id", m.Members["VMId"].Value)
                            .AddCommand("Get-VMHardDiskDrive")
                            .Invoke();
                    }).ToArray(), (x) => { return x.Members["Path"].Value.ToString(); }),
                    Type = VirtualMachineType.NONE
                });
            }
            return machines;
        }

        public static void NewTemplate(string hostName, string name, Guid baseUid, string switchName, JToken config)
        {
            var baseVm = SessionManager.GetDatabase().GetVm(hostName, baseUid);
            string vhdPath = Path.Combine(Settings.Default.vhdPath, hostName + "\\" + baseUid.ToString() + ".vhdx");
            if (baseVm.Host != hostName)
                CopyFile(hostName, baseVm.Host, baseVm.VhdPath[0], vhdPath);
            PSWrapper.Execute(hostName, config, null, name, vhdPath, switchName);
        }

        #endregion
    }
}
