using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trhvmgr.Objects
{
    public class VirtualMachine
    {
        public string Name { get; set; }
        public Guid UUID { get; set; }
        public string Host { get; set; }
    }
}
