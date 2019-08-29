using System;
using System.Management.Automation;
using System.Windows.Forms;

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
                ProgressRecord newRecord = ((PSDataCollection<ProgressRecord>)o)[e.Index];
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

        public static PsStreamEventHandlers GetUIHandlers(WorkerContext ctx) => new PsStreamEventHandlers()
        {
            Debug = (o, e) =>
            {
                DebugRecord newRecord = ((PSDataCollection<DebugRecord>)o)[e.Index];
                if (!string.IsNullOrEmpty(newRecord.Message))
                    Program.MainFrmInstance.SetStatus(newRecord.Message);
            },
            Error = (o, ev) =>
            {
                ErrorRecord newRecord = ((PSDataCollection<ErrorRecord>)o)[ev.Index];
                MessageBox.Show(newRecord.ToString(), "Powershell Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            },
            Information = (o, e) =>
            {
                InformationalRecord newRecord = ((PSDataCollection<InformationalRecord>)o)[e.Index];
                if (!string.IsNullOrEmpty(newRecord.Message))
                    Program.MainFrmInstance.SetStatus(newRecord.Message);
            },
            Progress = (o, ev) =>
            {
                ProgressRecord r = ((PSDataCollection<ProgressRecord>)o)[ev.Index];
                ctx.d.ReportCaption(r.Activity);
                if (r.PercentComplete >= 0 && r.PercentComplete <= 100)
                    ctx.d.ReportProgress(r.PercentComplete);
            },
            Verbose = (o, e) =>
            {
                VerboseRecord newRecord = ((PSDataCollection<VerboseRecord>)o)[e.Index];
                if(!string.IsNullOrEmpty(newRecord.Message))
                    Program.MainFrmInstance.SetStatus(newRecord.Message);
            },
            Warning = (o, e) =>
            {
                WarningRecord newRecord = ((PSDataCollection<WarningRecord>)o)[e.Index];
                if (!string.IsNullOrEmpty(newRecord.Message))
                    Program.MainFrmInstance.SetStatus(newRecord.Message);
            }
        };

        public static PsStreamEventHandlers GetUIHandlers() => new PsStreamEventHandlers()
        {
            Debug = (o, e) =>
            {
                DebugRecord newRecord = ((PSDataCollection<DebugRecord>)o)[e.Index];
                if (!string.IsNullOrEmpty(newRecord.Message))
                    Program.MainFrmInstance.SetStatus(newRecord.Message);
            },
            Error = (o, ev) =>
            {
                ErrorRecord newRecord = ((PSDataCollection<ErrorRecord>)o)[ev.Index];
                MessageBox.Show(newRecord.ToString(), "Powershell Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            },
            Information = (o, e) =>
            {
                InformationalRecord newRecord = ((PSDataCollection<InformationalRecord>)o)[e.Index];
                if (!string.IsNullOrEmpty(newRecord.Message))
                    Program.MainFrmInstance.SetStatus(newRecord.Message);
            },
            Progress = (o, ev) =>
            {
                ProgressRecord r = ((PSDataCollection<ProgressRecord>)o)[ev.Index];
                Program.MainFrmInstance.SetStatus(r.Activity);
                /*if (r.PercentComplete >= 0 && r.PercentComplete <= 100)
                    ctx.d.ReportProgress(r.PercentComplete);*/
            },
            Verbose = (o, e) =>
            {
                VerboseRecord newRecord = ((PSDataCollection<VerboseRecord>)o)[e.Index];
                if (!string.IsNullOrEmpty(newRecord.Message))
                    Program.MainFrmInstance.SetStatus(newRecord.Message);
            },
            Warning = (o, e) =>
            {
                WarningRecord newRecord = ((PSDataCollection<WarningRecord>)o)[e.Index];
                if (!string.IsNullOrEmpty(newRecord.Message))
                    Program.MainFrmInstance.SetStatus(newRecord.Message);
            }
        };

        internal static void ClearHandlers(PowerShell ps)
        {
            throw new NotImplementedException();
        }

        public static void RegisterHandlers(PowerShell ps, PsStreamEventHandlers handlers)
        {
            ps.Streams.Debug.DataAdded += handlers?.Debug;
            ps.Streams.Error.DataAdded += handlers?.Error;
            ps.Streams.Information.DataAdded += handlers?.Information;
            ps.Streams.Progress.DataAdded += handlers?.Progress;
            ps.Streams.Verbose.DataAdded += handlers?.Verbose;
            ps.Streams.Warning.DataAdded += handlers?.Warning;
        }

        public static void ClearHandlers(PowerShell ps, PsStreamEventHandlers handlers)
        {
            ps.Streams.Debug.DataAdded -= handlers?.Debug;
            ps.Streams.Error.DataAdded -= handlers?.Error;
            ps.Streams.Information.DataAdded -= handlers?.Information;
            ps.Streams.Progress.DataAdded -= handlers?.Progress;
            ps.Streams.Verbose.DataAdded -= handlers?.Verbose;
            ps.Streams.Warning.DataAdded -= handlers?.Warning;
        }
    }
}
