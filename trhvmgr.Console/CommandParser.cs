using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using trhvmgr.Lib;
using trhvmgr.Plugs;

namespace trhvmgr.Interactive
{
    public class CommandParser
    {
        #region Utils

        private static int GetSelectedIndex()
        {
            Console.Write("Enter selected index: ");
            int _idx = -1;
            while (!int.TryParse(Console.ReadLine(), out _idx))
                Console.Write("Enter selected index: ");
            return _idx;
        }

        static int tableWidth = 110;

        static void PrintLine()
        {
            Console.WriteLine("|" + new string('-', tableWidth-3) + "|");
        }

        static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        static string AlignCentre(string text, int width)
        {
            if (text == null) text = "";
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }

        #endregion

        [CommandInfo(0, 1, ParameterSyntax = "[HostName]", HelpText = "")]
        public static void getvms(string[] tokens)
        {
            string server = Dns.GetHostName();
            if (tokens.Length == 2) server = tokens[1];
            foreach (var obj in HyperV.GetPsVm(server))
                Console.WriteLine(obj.ToString());
        }

        [CommandInfo(0, 1, ParameterSyntax = "[HostName]", HelpText = "")]
        public static void gethostadapters(string[] tokens)
        {
            string server = Dns.GetHostName();
            if (tokens.Length == 2) server = tokens[1];
            var adapters = NetAdapter.GetNetAdapter(server);
            PrintRow(new string[] { "Name", "InterfaceDescription", "MacAddress", "Status" });
            PrintLine();
            foreach (var obj in adapters)
            {
                PrintRow(new string[]
                {
                    (string)obj.Members["Name"].Value,
                    (string)obj.Members["InterfaceDescription"].Value,
                    (string)obj.Members["MacAddress"].Value,
                    (string)obj.Members["ifOperStatus"].Value
                });
            }
            Console.WriteLine();
            foreach (var obj in adapters)
                Console.WriteLine(obj);
        }

        [CommandInfo(0, 1, ParameterSyntax = "[HostName]", HelpText = "")]
        public static void getswitches(string[] tokens)
        {
            string server = Dns.GetHostName();
            if (tokens.Length == 2) server = tokens[1];
            PrintRow(new string[] { "Name", "NetAdapterInterfaceDescription", "SwitchType", "Id" });
            PrintLine();
            var switches = HyperV.GetVmSwitch(server);
            foreach (var obj in switches)
            {
                if ((string)obj.Members["SwitchType"].Value == "External")
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Gray;
                PrintRow(new string[]
                {
                    (string)obj.Members["Name"].Value,
                    (string)obj.Members["NetAdapterInterfaceDescription"].Value,
                    (string)obj.Members["SwitchType"].Value,
                    ((Guid)obj.Members["Id"].Value).ToString()
                });
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine();
            foreach (var obj in switches)
                Console.WriteLine(obj);
        }

        [CommandInfo(0, 1, ParameterSyntax = "[HostName]", HelpText = "")]
        public static void newvm(string[] tokens)
        {
            // Get user input
            string server = Dns.GetHostName();
            if (tokens.Length == 2) server = tokens[1];
            string name, vhdpath, vmswitch;
            Console.Write("Name: ");
            name = Console.ReadLine();
            Console.WriteLine($@"Default VHD Path is: C:\VHDs\{name}.vhdx");
            Console.Write("VHD Name (leave blank for default): ");
            vhdpath = Console.ReadLine();
            if (string.IsNullOrEmpty(vhdpath)) vhdpath = name;
            Console.WriteLine("Available switches:");
            var switches = HyperV.GetVmSwitch(server);
            for (int i = 0; i < switches.Count; i++)
                Console.WriteLine($"[{i}] { switches[i].Members["Name"].Value }");
            vmswitch = switches[GetSelectedIndex()].Members["Name"].Value.ToString();

            // Parse JSON
            using (StreamReader reader = File.OpenText(@"Config\templates.json"))
            {
                JObject doc = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                Console.WriteLine("All available VM configurations:");
                for (int i = 0; i < doc["templates"].Count(); i++)
                    Console.WriteLine($"[{ i }] { doc["templates"][i]["name"] }");
                var config = doc["templates"][GetSelectedIndex()];
                PSWrapper.Execute(server, config, PsStreamEventHandlers.DefaultHandlers, name, vhdpath, vmswitch);
            }
        }

        [CommandInfo(0, 1, ParameterSyntax = "[HostName]", HelpText = "")]
        public static void wake(string[] tokens)
        {
            string server = Dns.GetHostName();
            if (tokens.Length == 2) server = tokens[1];
            Console.WriteLine("Waking up...");
            Interface.BringOnline(server, PsStreamEventHandlers.DefaultHandlers);
            Console.WriteLine("Restarting vmms...");
            Interface.RestartService(server, PsStreamEventHandlers.DefaultHandlers);
        }

        [CommandInfo(0, 1, ParameterSyntax = "[HostName]", HelpText = "Interactive applet to add a switch")]
        public static void newvmswitch(string[] tokens)
        {
            string server = Dns.GetHostName();
            if (tokens.Length == 2) server = tokens[1];
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Choose an adapter:");
            var adapters = NetAdapter.GetNetAdapter(server);
            for (int i = 0; i < adapters.Count; i++)
                Console.WriteLine($"[{i}] ({ adapters[i].Members["Name"].Value }) { adapters[i].Members["InterfaceDescription"].Value }");
            var ada = adapters[GetSelectedIndex()].Members["InterfaceDescription"].Value.ToString();
            PSWrapper.Execute(server, (ps) => {
                return ps.AddCommand("New-VMSwitch")
                    .AddParameter("Name", name)
                    .AddParameter("NetAdapterInterfaceDescription", ada)
                    .AddParameter("AllowManagementOS", true)
                .Invoke();
            }, PsStreamEventHandlers.DefaultHandlers);
        }
    }
}
