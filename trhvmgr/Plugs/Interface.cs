using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trhvmgr.Objects;

namespace trhvmgr.Plugs
{
    public class Interface
    {
        #region Virtual Machine State Query

        public static List<VirtualMachine> GetVms(string hostName)
        {
            List<VirtualMachine> machines = new List<VirtualMachine>();
            machines.Add(new VirtualMachine {
                Host = hostName,
                Name = "Test Machine A",
                Uuid = Guid.NewGuid(),
                VhdPath = "C:\\Path\\To\\Machine\\A"
            });
            machines.Add(new VirtualMachine
            {
                Host = hostName,
                Name = "Test Machine B",
                Uuid = Guid.NewGuid(),
                VhdPath = "C:\\Path\\To\\Machine\\B"
            });
            machines.Add(new VirtualMachine
            {
                Host = hostName,
                Name = "Test Machine C",
                Uuid = Guid.NewGuid(),
                VhdPath = "C:\\Path\\To\\Machine\\C"
            });
            return machines;
        }

        #endregion
    }
}
