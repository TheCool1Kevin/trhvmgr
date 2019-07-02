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
        public string Host { get; set; }
        public string VhdPath { get; set; }
        public Guid Uuid { get; set; }
    }

    public class DbVirtualMachine
    {
        public string Host { get; set; }
        [BsonId]
        public Guid Uuid { get; set; }
    }
}
