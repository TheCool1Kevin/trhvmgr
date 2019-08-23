using Newtonsoft.Json.Linq;
using System;
using System.Collections;
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

        public static void CopyFile(string srcHost, string dstHost, string src, string dst, PsStreamEventHandlers handlers = null)
        {
            PSWrapper.Execute(dstHost, (ps) =>
            {
                ps.AddCommand("Set-Variable").AddParameter("Name", "cred").AddParameter("Value", SessionManager.GetCredential());
                ps.AddScript($"Copy-Item \"{src}\" \"{dst}\" -Force –FromSession (New-PSSession –ComputerName {srcHost} -Credential $cred)");
                ps.Invoke();
                return null;
            }, handlers);
        }

        public static void StartBitsTransfer(string srcHost, string dstHost, string src, string dst, PsStreamEventHandlers handlers = null)
        {
            PSWrapper.Execute(dstHost, (ps) =>
            {
                ps.AddCommand("Set-Variable").AddParameter("Name", "cred").AddParameter("Value", SessionManager.GetCredential());
                ps.AddScript($"Start-BitsTransfer -Credential $cred -Source {src} -Destination {dst}");
                ps.Invoke();
                return null;
            });
        }

        public static void NewDirectory(string host, string path, PsStreamEventHandlers handlers = null)
        {
            PSWrapper.Execute(host, (ps) =>
            {
                return ps.AddCommand("New-Item")
                    .AddParameter("Type", "Directory")
                    .AddParameter("Path", path)
                    .AddParameter("Force").Invoke();
            }, handlers);
        }
        
        #endregion

        #region Virtual Machine State Query

        /// <exception cref="Exception">May throw exceptions</exception>
        public static List<VirtualMachine> GetVms(string hostName, PsStreamEventHandlers handlers = null)
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

        public static void NewTemplate(string hostName, string name, Guid baseUid, string switchName, JToken config, PsStreamEventHandlers handlers = null)
        {
            var baseVm = SessionManager.GetDatabase().GetVm(baseUid);
            var srcHost = baseVm.Host;
            var dstHost = hostName;
            string dstDir = Path.Combine(Settings.Default.vhdPath, srcHost);
            string dstPath = Path.Combine(dstDir, baseUid.ToString() + ".vhdx");
            if (baseVm.Host != hostName)
            {
                NewDirectory(dstHost, dstDir, handlers);
                CopyFile(srcHost, dstHost, baseVm.VhdPath[0], dstPath, handlers);
            }
            PSWrapper.Execute(dstHost, config, null, name, dstPath, switchName);
        }

        #endregion
    }
}
