using LiteDB;
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
        public string MacAddress { get; set; }
        public string IpAddress { get; set; }
        public List<Guid> VirtualMachines = new List<Guid>();
        public List<string> VirtualHardDisks = new List<string>();
    }

    public class DbHostComputer
    {
        [BsonId]
        public string HostName { get; set; }
    }
}
