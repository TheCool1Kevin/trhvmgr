using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trhvmgr.Objects
{
    public class HostComputer
    {
        public string HostName { get; set; }
        public List<Guid> VMs; // UUID of all virtual machines
    }
}
