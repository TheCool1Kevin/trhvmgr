using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using trhvmgr.Plugs;

namespace trhvmgr.Lib
{
    /// <summary>
    /// Executes PowerShell commands.
    /// </summary>
    public class PSWrapper
    {
        public static bool Execute(string host, string command, Dictionary<string, string> parameters)
        {
            Runspace runspace = Interface.GetRunspace(host);
            runspace.Open();
            using (PowerShell ps = PowerShell.Create())
            {
                ps.Runspace = runspace;
                ps.AddStatement()
                    .AddCommand(command)
                    .AddParameters(parameters);
                ps.Invoke();
                if (ps.HadErrors) return false;
            }
            return true;
        }

        public static PSObject Execute(string host, Func<PowerShell, PSObject> func)
        {
            Runspace runspace = Interface.GetRunspace(host);
            runspace.Open();
            using (PowerShell ps = PowerShell.Create())
            {
                return func(ps);
            }
        }

        public static void Execute(string host, JToken cfg, params object[] args)
        {
            Guid vmid;
            foreach(var cmd in cfg["actions"])
            {
                bool? a = cmd["pipevmid"]?.Value<bool>();
                string ps = cmd["ps"].Value<string>();
                var pa = JsonConvert.DeserializeObject<Dictionary<string, object>>(cmd["params"].ToString());
                foreach(var k in JsonConvert.DeserializeObject<Dictionary<string, object>>(cmd["params"].ToString()).Keys)
                {
                    if (pa[k] is string)
                        pa[k] = string.Format(pa[k] as string, args);
                }
            }
        }
    }
}
