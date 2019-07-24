using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static bool Execute(string host, string command, out Collection<PSObject> res, Dictionary<string, string> parameters = null)
        {
            Runspace runspace = Interface.GetRunspace(host);
            runspace.Open();
            using (PowerShell ps = PowerShell.Create())
            {
                ps.Runspace = runspace;
                ps.AddStatement()
                    .AddCommand(command);
                if(parameters != null)
                    ps.AddParameters(parameters);
                res = ps.Invoke();
                if (ps.HadErrors) return false;
            }
            return true;
        }

        public static Collection<PSObject> Execute(string host, Func<PowerShell, Collection<PSObject>> func)
        {
            Runspace runspace = Interface.GetRunspace(host);
            runspace.Open();
            using (PowerShell ps = PowerShell.Create())
            {
                ps.Runspace = runspace;
                return func(ps);
            }
        }

        public static void Execute(string host, JToken cfg, params object[] args)
        {
            PSObject vmid = null;
            foreach(var cmd in cfg["actions"])
            {
                bool? a = cmd["pipevmid"]?.Value<bool>();
                bool? b = cmd["getvm"]?.Value<bool>();
                string ps = cmd["ps"].Value<string>();
                var pa = JsonConvert.DeserializeObject<Dictionary<string, object>>(cmd["params"].ToString());
                foreach(var k in JsonConvert.DeserializeObject<Dictionary<string, object>>(cmd["params"].ToString()).Keys)
                    if (pa[k] is string)
                        pa[k] = string.Format(pa[k] as string, args);
                var res = Execute(host, (pss) =>
                {
                    if (b == true)
                        pss.AddStatement().AddCommand("Get-VM").AddParameter("Id", vmid.Members["Id"].Value);
                    pss.AddCommand(ps).AddParameters(pa);
                    return pss.Invoke();
                });
                if (a == true) vmid = res[0];
            }
        }
    }
}
