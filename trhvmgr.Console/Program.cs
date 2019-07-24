using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace trhvmgr.Interactive
{
    class Program
    {
        static SecureString GetPassword()
        {
            SecureString password = new SecureString();
            ConsoleKeyInfo nextKey = Console.ReadKey(true);

            while (nextKey.Key != ConsoleKey.Enter)
            {
                if (nextKey.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password.RemoveAt(password.Length - 1);
                        Console.Write(nextKey.KeyChar);
                        Console.Write(" ");
                        Console.Write(nextKey.KeyChar);
                    }
                }
                else
                {
                    password.AppendChar(nextKey.KeyChar);
                    Console.Write("*");
                }
                nextKey = Console.ReadKey(true);
            }
            Console.WriteLine();
            password.MakeReadOnly();
            return password;
        }

        static string[] ParseTokens(string cmd)
        {
            List<string> tokens = new List<string>();
            bool quote = false, escape = false;
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < cmd.Length; i++)
            {
                if (escape)
                    escape = false;
                else if (cmd[i] == '\\' && quote)
                    escape = true;
                else if (cmd[i] == '"')
                {
                    quote = !quote;
                    continue;
                }
                else if (cmd[i] == ' ' && !quote)
                {
                    tokens.Add((string)sb.ToString().Trim().Clone());
                    sb.Clear();
                    continue;
                }
                
                sb.Append(cmd[i]);
            }
            tokens.Add((string)sb.ToString().Trim().Clone());
            return tokens.ToArray();
        }

        // Here, we don't use Action<string[]> because we need the attributes
        static Dictionary<string, MethodBase> CommandRegistry = new Dictionary<string, MethodBase>();

        static void RunCommand(string[] tokens)
        {
            MethodBase action = CommandRegistry[tokens[0].ToLower()];
            CommandInfo attr = null;
            foreach(var a in action.GetCustomAttributes(true))
            {
                if (a is CommandInfo)
                {
                    attr = a as CommandInfo;
                    break;
                }
            }
            if (attr == null)
            {
                Console.WriteLine($"Error: Command { tokens[0].ToUpper() } registered improperly!");
                return;
            }

            if(tokens.Length == 2 && tokens[1] == "?")
            {
                Console.WriteLine($"Usage: { tokens[0].ToUpper() } { attr.ParameterSyntax }");
                Console.WriteLine(attr.HelpText);
            }
            else if (tokens.Length <= attr.MaxParams + 1 && tokens.Length <= attr.MaxParams + 1)
            {
                try
                {
                    action.Invoke(null, new object[] { tokens });
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.InnerException.Message);
                }
            }
            else
                Console.WriteLine($"Invalid syntax. Usage: { tokens[0].ToUpper() } { attr.ParameterSyntax }");
        }

        static int __zklen__ = 0;
        static void RegisterCommand(string name)
        {
            CommandRegistry[name] = typeof(CommandParser).GetMethod(name);
            __zklen__ = Math.Max(__zklen__, name.Length + 1);
        }

        static void Main(string[] args)
        {
            // Register commands
            RegisterCommand("getvms");
            RegisterCommand("gethostadapters");
            RegisterCommand("getswitches");
            RegisterCommand("newvm");
            RegisterCommand("wake");

            // Display welcoming UI
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Username: ");
            string usr = Console.ReadLine();
            Console.Write("Password: ");
            SecureString psw = GetPassword();

            SessionManager.Instance.SetCredential(new PSCredential(usr, psw));
            SessionManager.Instance.InitializeDatabase(ConfigurationManager.ConnectionStrings["ServerDB"].ConnectionString);

            Console.WriteLine("Entered management console. Type commands after '>' and press return to submit.");
            Console.WriteLine("Type '?' for help. Type 'q' to quit.");

            bool exit = false;
            while(!exit)
            {
                Console.WriteLine();
                Console.Write("> ");
                string[] tokens = ParseTokens(Console.ReadLine());
                switch (tokens[0].ToLowerInvariant())
                {
                    case "q":
                        exit = true;
                        continue;
                    case "?":
                        Console.WriteLine("Type '?' to show this message.");
                        Console.WriteLine("Type COMMAND ? to show help message for a specific command.");
                        Console.WriteLine("Syntax: COMMAND param1 param1 [opt_param1] [opt_param2]");
                        Console.WriteLine();
                        foreach(var c in CommandRegistry.Keys)
                        {
                            MethodBase action = CommandRegistry[c];
                            CommandInfo attr = null;
                            foreach (var a in action.GetCustomAttributes(true))
                            {
                                if (a is CommandInfo)
                                {
                                    attr = a as CommandInfo;
                                    break;
                                }
                            }
                            if (attr == null)
                            {
                                Console.WriteLine($"Error: Command { c.ToUpper() } registered improperly!");
                                return;
                            }

                            Console.WriteLine($"Command: { c.ToUpper().PadRight(__zklen__, ' ') } { attr.ParameterSyntax }");
                        }
                        break;
                    default:
                        if (CommandRegistry.ContainsKey(tokens[0].ToLower()))
                            RunCommand(tokens);
                        else
                            Console.WriteLine("Unknown command.");
                        break;
                }
                exit = false;
            }
        }
    }
}
