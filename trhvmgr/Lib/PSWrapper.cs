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
    public class PsStreamEventHandlers
    {
        public EventHandler<DataAddedEventArgs> Debug { get; set; }
        public EventHandler<DataAddedEventArgs> Error { get; set; }
        public EventHandler<DataAddedEventArgs> Information { get; set; }
        public EventHandler<DataAddedEventArgs> Progress { get; set; }
        public EventHandler<DataAddedEventArgs> Verbose { get; set; }
        public EventHandler<DataAddedEventArgs> Warning { get; set; }

        public static PsStreamEventHandlers DefaultHandlers => new PsStreamEventHandlers()
        {
            Debug = (o, e) =>
            {
                DebugRecord newRecord = ((PSDataCollection<DebugRecord>)o)[e.Index];
                Console.WriteLine(newRecord.Message);
            },
            Error = (o, e) =>
            {
                ErrorRecord newRecord = ((PSDataCollection<ErrorRecord>)o)[e.Index];
                Console.WriteLine(newRecord.ToString());
            },
            Information = (o, e) =>
            {
                InformationalRecord newRecord = ((PSDataCollection<InformationalRecord>)o)[e.Index];
                Console.WriteLine(newRecord.Message);
            },
            Progress = (o, e) =>
            {
                ProgressRecord newRecord = ((PSDataCollection<ProgressRecord>) o)[e.Index];
                if (newRecord.PercentComplete != 0 && newRecord.PercentComplete != 100 && newRecord.PercentComplete != -1)
                    Console.WriteLine($"Progress updated: {newRecord.StatusDescription}");
            },
            Verbose = (o, e) => 
            {
                VerboseRecord newRecord = ((PSDataCollection<VerboseRecord>)o)[e.Index];
                Console.WriteLine(newRecord.Message);
            },
            Warning = (o, e) =>
            {
                WarningRecord newRecord = ((PSDataCollection<WarningRecord>)o)[e.Index];
                Console.WriteLine(newRecord.Message);
            }
        };

        public static void RegisterHandlers(PowerShell ps, PsStreamEventHandlers handlers)
        {
            ps.Streams.Debug.DataAdded += handlers?.Debug;
            ps.Streams.Error.DataAdded += handlers?.Error;
            ps.Streams.Information.DataAdded += handlers?.Information;
            ps.Streams.Progress.DataAdded += handlers?.Progress;
            ps.Streams.Verbose.DataAdded += handlers?.Verbose;
            ps.Streams.Warning.DataAdded += handlers?.Warning;
        }
    }

    /// <summary>
    /// Executes PowerShell commands.
    /// </summary>
    public class PSWrapper
    {
        private static Runspace GetCachedRunspace(string host)
        {
            return Interface.GetRunspace(host);
        }

        public static bool Execute(string host, string command, out Collection<PSObject> res, Dictionary<string, string> parameters = null)
        {
            Runspace runspace = Interface.GetRunspace(host);
            runspace.Open();
            bool status = false;

            try
            {
                using (PowerShell ps = PowerShell.Create())
                {
                    ps.Runspace = runspace;
                    ps.AddStatement()
                        .AddCommand(command);
                    if (parameters != null)
                        ps.AddParameters(parameters);
                    res = ps.Invoke();
                    status = !ps.HadErrors;
                }
            }
            catch(Exception e)
            {
                runspace.Close();
                runspace.Dispose();
                throw e;
            }

            runspace.Close();
            runspace.Dispose();

            return status;
        }

        public static Collection<PSObject> Execute(string host, Func<PowerShell, Collection<PSObject>> func, PsStreamEventHandlers handlers = null)
        {
            Runspace runspace = Interface.GetRunspace(host);
            runspace.Open();
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
            catch(Exception e)
            {
                runspace.Close();
                runspace.Dispose();
                throw e;
            }

            runspace.Close();
            runspace.Dispose();
            return res;
        }

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
                    else pss.AddCommand(ps).AddParameters(pa); ;
                    return pss.Invoke();
                });
                if (a == true) vmid = res[0];
            }
        }
    }
}
