using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trhvmgr.Objects
{
    public enum VirtualMachineType
    {
        NONE = 0,
        BASE,
        TEMPLATE,
        DEPLOY
    };

    public class VirtualMachine
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public string[] VhdPath { get; set; }
        public Guid Uuid { get; set; }
        public VirtualMachineType Type { get; set; }
    }

    public class DbVirtualMachine
    {
        public string Host { get; set; }
        [BsonId]
        public Guid Uuid { get; set; }
        public int VmType { get; set; }
    }
}
