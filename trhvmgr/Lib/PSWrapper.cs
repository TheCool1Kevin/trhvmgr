using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Windows.Forms;
using trhvmgr.Plugs;

namespace trhvmgr.Lib
{
    /// <summary>
    /// Executes PowerShell commands.
    /// </summary>
    public class PSWrapper
    {
        #region Execute

        /// <summary>
        /// Returns true on success.
        /// </summary>
        /// <exception cref="Exception">Throws exceptions</exception>
        public static bool Execute(string host, string command, out Collection<PSObject> res, Dictionary<string, string> parameters, PsStreamEventHandlers handlers = null)
        {
            bool status = false;
            res = Execute(host, (ps) =>
            {
                ps.AddStatement()
                        .AddCommand(command);
                if (parameters != null)
                    ps.AddParameters(parameters);
                var foo = ps.Invoke();
                status = !ps.HadErrors;
                return foo;
            }, handlers);
            return status;
        }

        /// <summary>
        /// Returns true on success.
        /// </summary>
        /// <exception cref="Exception">Throws exceptions</exception>
        public static bool Execute(string host, string scriptblock, out Collection<PSObject> res, PsStreamEventHandlers handlers = null)
        {
            bool status = false;
            res = Execute(host, (ps) =>
            {
                ps.AddStatement()
                    .AddScript(scriptblock);
                var foo = ps.Invoke();
                status = !ps.HadErrors;
                return foo;
            }, handlers);
            return status;
        }

        /// <summary>
        /// Returns a collection of Powershell objects, or null on failure.
        /// </summary>
        /// <exception cref="Exception">Throws exceptions</exception>
        public static Collection<PSObject> Execute(string host, Func<PowerShell, Collection<PSObject>> func, PsStreamEventHandlers handlers = null)
        {
            Runspace runspace = Interface.GetRunspace(host);
            //runspace.Open();
            Collection<PSObject> res = null;
            try
            {
                using (PowerShell ps = PowerShell.Create())
                {
                    PsStreamEventHandlers.RegisterHandlers(ps, handlers);
                    ps.Runspace = runspace;
                    res = func(ps);
                }
            }
            catch(Exception)
            {
                //runspace.Dispose();
                throw;
            }

            //runspace.Dispose();
            return res;
        }

        /// <summary>
        /// Executes Powershell from special JSON instructions.
        /// </summary>
        /// <exception cref="Exception">Throws exceptions</exception>
        public static void Execute(string host, JToken cfg, PsStreamEventHandlers handlers, params object[] args)
        {
            PSObject vmid = null;
            foreach(var cmd in cfg["actions"])
            {
                bool? a = cmd["pipevmid"]?.Value<bool>();
                bool? b = cmd["getvm"]?.Value<bool>();
                bool? s = cmd["isscript"]?.Value<bool>();
                string sb = string.Format(cmd["scriptblock"]?.Value<string>() ?? "", args);
                string ps = cmd["ps"]?.Value<string>() ?? "";
                Dictionary<string, object> pa = null;
                if (s != true)
                {
                    pa = JsonConvert.DeserializeObject<Dictionary<string, object>>(cmd["params"].ToString());
                    foreach (var k in JsonConvert.DeserializeObject<Dictionary<string, object>>(cmd["params"].ToString()).Keys)
                    {
                        if (pa[k] is string)
                            pa[k] = string.Format(pa[k] as string, args);
                        else if (pa[k] is JArray)
                            pa[k] = (pa[k] as JArray).ToObject<string[]>();
                    }
                }

                var res = Execute(host, (pss) =>
                {
                    PsStreamEventHandlers.RegisterHandlers(pss, handlers);
                    if (b == true) pss.AddStatement().AddCommand("Get-VM").AddParameter("Id", vmid.Members["Id"].Value);
                    if (s == true) pss.AddScript(ps + " " + sb);
                    else pss.AddCommand(ps).AddParameters(pa);
                    return pss.Invoke();
                });
                if (a == true) vmid = res[0];
            }
        }

        #endregion

        #region Fast Execute

        private static Dictionary<string, PowerShell> cachedPowershell = new Dictionary<string, PowerShell>();

        public static Collection<PSObject> FastExecute(string host, Func<PowerShell, Collection<PSObject>> func, PsStreamEventHandlers handlers = null)
        {
            Collection<PSObject> res = null;
            PowerShell ps = null;
            if (cachedPowershell.ContainsKey(host))
                ps = cachedPowershell[host];
            else
            {
                cachedPowershell[host] = PowerShell.Create();
                ps = cachedPowershell[host];
                ps.Runspace = Interface.GetRunspace(host);
            }
            //PsStreamEventHandlers.RegisterHandlers(ps, handlers);

            try
            {
                ps.Stop();
                ps.Commands.Clear();
                res = func(ps);
            }
            catch (Exception)
            {
                throw;
            }
            
            return res;
        }

        public static bool FastExecute(string host, string scriptblock, out Collection<PSObject> res, PsStreamEventHandlers handlers = null)
        {
            bool status = false;
            res = FastExecute(host, (ps) =>
            {
                ps.AddStatement()
                    .AddScript(scriptblock);
                var foo = ps.Invoke();
                status = !ps.HadErrors;
                return foo;
            }, handlers);
            return status;
        }

        public static void FlushCache(string host)
        {
            if (!cachedPowershell.ContainsKey(host)) return;
            cachedPowershell[host].Dispose();
            cachedPowershell[host] = null;
            cachedPowershell.Remove(host);
        }

        public static void FlushCache()
        {
            foreach(var p in cachedPowershell.Values)
                p.Dispose();
            foreach (var k in cachedPowershell.Keys)
                cachedPowershell[k] = null;
            cachedPowershell.Clear();
        }

        #endregion
    }
}
