using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using trhvmgr.Plugs;
using trhvmgr.Lib;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;

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

        #endregion

        [CommandInfo(0, 1, ParameterSyntax = "[HostName]", HelpText = "")]
        public static void getvms(string[] tokens)
        {
            string server = Dns.GetHostName();
            if (tokens.Length == 2) server = tokens[1];
            foreach (var obj in HyperV.GetVm(server))
                Console.WriteLine(obj.ToString());
        }

        [CommandInfo(0, 1, ParameterSyntax = "[HostName]", HelpText = "")]
        public static void gethostadapters(string[] tokens)
        {
            string server = Dns.GetHostName();
            if (tokens.Length == 2) server = tokens[1];
            foreach (var obj in NetAdapter.GetNetAdapter(server))
            {
                Console.WriteLine($"[Name=\"{(string)obj.Members["Name"].Value}\"] [InterfaceDescription=\"{(string)obj.Members["InterfaceDescription"].Value}\"] " +
                    $"[MacAddress={(string)obj.Members["MacAddress"].Value}] [Status={(string)obj.Members["ifOperStatus"].Value}]");

            }
        }

        [CommandInfo(0, 1, ParameterSyntax = "[HostName]", HelpText = "")]
        public static void getswitches(string[] tokens)
        {
            string server = Dns.GetHostName();
            if (tokens.Length == 2) server = tokens[1];
            foreach (var obj in HyperV.GetVmSwitch(server))
            {
                if ((string)obj.Members["SwitchType"].Value == "External")
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"[Name=\"{(string)obj.Members["Name"].Value}\"] [NetAdapterInterfaceDescription=\"{(string)obj.Members["NetAdapterInterfaceDescription"].Value}\"] " +
                    $"[SwitchType={(string)obj.Members["SwitchType"].Value}] [Id={(Guid)obj.Members["Id"].Value}]");
                Console.ForegroundColor = ConsoleColor.White;
            }
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
                PSWrapper.Execute(server, config);
            }
        }
    }
}
