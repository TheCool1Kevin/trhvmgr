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

        private static Dictionary<Tuple<string, PSCredential>, Runspace> @cachedRunspaces = new Dictionary<Tuple<string, PSCredential>, Runspace>();

        public static Runspace CreateNewRunspace(string hostName, PSCredential cred)
        {
            WSManConnectionInfo ci = new WSManConnectionInfo();
            ci.ComputerName = hostName;
            ci.AuthenticationMechanism = AuthenticationMechanism.Kerberos;
            ci.Credential = cred;
            Runspace runspace = RunspaceFactory.CreateRunspace(ci);
            return runspace;
        }

        public static Runspace CreateNewRunspace(string hostName)
        {
            return CreateNewRunspace(hostName, SessionManager.GetCredential());
        }

        public static Runspace GetRunspace(string hostName, PSCredential cred, bool open = true)
        {
            var key = Tuple.Create(hostName, cred);
            Runspace rs = null;
            if (cachedRunspaces.ContainsKey(key))
                rs = cachedRunspaces[key];
            else
            {
                rs = CreateNewRunspace(hostName, cred);
                cachedRunspaces[key] = rs;
            }

            if (rs.RunspaceStateInfo.State == RunspaceState.BeforeOpen)
                rs.Open();

            return rs;
        }

        public static Runspace GetRunspace(string hostName, bool open = true)
        {
            return GetRunspace(hostName, SessionManager.GetCredential(), open);
        }

        /// <exception cref="Exception">May throw exceptions</exception>
        public static void BringOnline(string hostName)
        {
            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddCommand("Set-Variable").AddParameter("Name", "cred").AddParameter("Value", SessionManager.GetCredential());
                ps.AddScript(
                    $"$ps = New-PSSession -ComputerName \"{hostName}\" -Credential $cred;" +
                    $"Remove-PSSession $ps;"
                ).Invoke();
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

        public static bool PathExists(string host, string path, PsStreamEventHandlers handlers = null)
        {
            return (bool) PSWrapper.Execute(host, (ps) =>
            {
                ps.AddCommand("Test-Path").AddParameter("path", path);
                return ps.Invoke();
            })[0].BaseObject;
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

        public static string[] GetChildItems(string host, string path, string filter = null)
        {
            Collection<PSObject> res = null;
            PSWrapper.Execute(host, $"Get-ChildItem -Path \"{path}\" -Filter \"{filter}\" -File -Recurse", out res);
            if (res == null || res.Count <= 0) return null;
            return res.ToList().Select(p => p.Members["FullName"].Value.ToString()).ToArray();
        }

        public static void DeleteItem(string host, string path)
        {
            Collection<PSObject> res = null;
            PSWrapper.FastExecute(host, $"Remove-Item –Path \"{path}\"", out res);
        }

        public static void StopService(string host, string svc, PsStreamEventHandlers handlers = null)
        {
            Collection<PSObject> res = null;
            PSWrapper.Execute(host, $"net stop \"{svc}\" | Write-Verbose -Verbose", out res, handlers);
        }

        public static void StartService(string host, string svc, PsStreamEventHandlers handlers = null)
        {
            Collection<PSObject> res = null;
            PSWrapper.Execute(host, $"net start \"{svc}\" | Write-Verbose -Verbose", out res, handlers);
        }

        #endregion

        #region Virtual Machines

        public static string GetTopMostParent(string hostName, string path, PsStreamEventHandlers handlers = null)
        {
            while (true)
            {
                var pso = HyperV.GetVhd(hostName, path, handlers)[0];
                if (pso == null) break;
                if (string.IsNullOrWhiteSpace((string)pso?.Members["ParentPath"].Value))
                    break;
                path = (string)pso?.Members["ParentPath"].Value;
            }
            return path;
        }

        public static void NewTemplate(string hostName, string name, Guid baseUid, string switchName, JToken config, PsStreamEventHandlers handlers = null)
        {
            var baseVm = SessionManager.GetDatabase().GetVm(baseUid);
            var srcHost = baseVm.Host;
            var dstHost = hostName;
            string dstDir = Path.Combine(Settings.Default.vhdPath, srcHost);
            string srcPath = GetTopMostParent(baseVm.Host, baseVm.VhdPath[0]);
            string dstPath = Path.Combine(dstDir, baseUid.ToString() + ".vhdx");
            string vhdPath = Path.Combine(Settings.Default.vhdPath, name + ".vhdx");

            if (srcHost != hostName)
            {
                if (!PathExists(dstHost, dstPath))
                {
                    NewDirectory(dstHost, dstDir, handlers);
                    CopyFile(srcHost, dstHost, srcPath, dstPath, handlers);
                }
            }
            else
                dstPath = srcPath;
            HyperV.NewVHD(dstHost, dstPath, vhdPath, handlers);
            PSWrapper.Execute(dstHost, config, handlers, name, vhdPath, switchName);

            var vm = HyperV.GetVm(hostName, name, handlers).GetDbObject();
            vm.ParentHost = srcHost;
            vm.ParentUuid = baseUid;
            vm.VmType = (int)VirtualMachineType.TEMPLATE;
            SessionManager.GetDatabase().SetVm(vm);
        }

        public static void NewDeployment(string hostName, string name, Guid tmplUid, string switchName, JToken config, PsStreamEventHandlers handlers = null)
        {
            var tmplVm = SessionManager.GetDatabase().GetVm(tmplUid);
            var baseVm = SessionManager.GetDatabase().GetVm(tmplVm.ParentUuid);
            if (baseVm == null)
            {
                throw new Exception("Template VM without parent.");
            }

            var bsrcHost = baseVm.Host;
            string bdstDir = Path.Combine(Settings.Default.vhdPath, bsrcHost);
            string bdstPath = Path.Combine(bdstDir, baseVm.Uuid.ToString() + ".vhdx");
            string bsrcPath = GetTopMostParent(baseVm.Host, baseVm.VhdPath[0]);

            var tsrcHost = tmplVm.Host;
            string tsrcPath = tmplVm.VhdPath[0];
            string dstHost = hostName;
            string vhdPath = Path.Combine(Settings.Default.vhdPath, name + ".vhdx");

            if (bsrcHost != hostName)
            {
                if (!PathExists(dstHost, bdstPath))
                {
                    NewDirectory(dstHost, bdstDir, handlers);
                    CopyFile(bsrcHost, dstHost, bsrcPath, bdstPath, handlers);
                }
            }
            else
                bdstPath = bsrcPath;

            if (!PathExists(dstHost, vhdPath))
            {
                NewDirectory(dstHost, bdstDir, handlers);
                CopyFile(tsrcHost, dstHost, tsrcPath, vhdPath, handlers);
            }
            HyperV.SetVHD(dstHost, bdstPath, vhdPath, true, handlers);
            PSWrapper.Execute(dstHost, config, handlers, name, vhdPath, switchName);

            var vm = HyperV.GetVm(hostName, name, handlers).GetDbObject();
            vm.ParentHost = bsrcHost;
            vm.ParentUuid = baseVm.Uuid;
            vm.VmType = (int)VirtualMachineType.DEPLOY;
            SessionManager.GetDatabase().SetVm(vm);
        }

        #endregion
    }
}
