using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using trhvmgr.Objects;

namespace trhvmgr.Plugs
{
    public class Interface
    {
        #region Powershell Credentials

        internal static Runspace GetRunspace(PSCredential cred, string hostName)
        {
            WSManConnectionInfo ci = new WSManConnectionInfo();
            ci.ComputerName = hostName;
            ci.AuthenticationMechanism = AuthenticationMechanism.Kerberos;
            ci.Credential = cred;
            Runspace runspace = RunspaceFactory.CreateRunspace(ci);
            return runspace;
        }

        internal static Runspace GetRunspace(string hostName)
        {
            return GetRunspace(SessionManager.Instance.PSCredential, hostName);
        }

        #endregion

        #region Virtual Machine State Query

        public static List<VirtualMachine> GetVms(string hostName)
        {
            List<VirtualMachine> machines = new List<VirtualMachine>();
            machines.Add(new VirtualMachine {
                Host = hostName,
                Name = "Test Machine A",
                Uuid = Guid.NewGuid(),
                VhdPath = "C:\\Path\\To\\Machine\\A",
                Type = VirtualMachineType.BASE
            });
            machines.Add(new VirtualMachine
            {
                Host = hostName,
                Name = "Test Machine B",
                Uuid = Guid.NewGuid(),
                VhdPath = "C:\\Path\\To\\Machine\\B",
                Type = VirtualMachineType.TEMPLATE
            });
            machines.Add(new VirtualMachine
            {
                Host = hostName,
                Name = "Test Machine C",
                Uuid = Guid.NewGuid(),
                VhdPath = "C:\\Path\\To\\Machine\\C",
                Type = VirtualMachineType.DEPLOY
            });
            return machines;
        }

        #endregion
    }
}
