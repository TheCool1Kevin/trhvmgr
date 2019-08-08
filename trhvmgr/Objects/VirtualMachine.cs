using LiteDB;
using System;
using trhvmgr.Database;

namespace trhvmgr.Objects
{
    public enum VirtualMachineType
    {
        NONE = 0,
        BASE,
        TEMPLATE,
        DEPLOY
    };

    public class VirtualMachine : ISerializableDbObject<DbVirtualMachine>
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public string[] VhdPath { get; set; }
        public Guid Uuid { get; set; }
        public VirtualMachineType Type { get; set; }

        public DbVirtualMachine GetDbObject()
        {
            return new DbVirtualMachine
            {
                Host = Host,
                Uuid = Uuid,
                VmType = (int) Type
            };
        }
    }

    [Database("vms")]
    public class DbVirtualMachine : IDbObject
    {
        public string Host { get; set; }
        [BsonId]
        public Guid Uuid { get; set; }
        public int VmType { get; set; }
    }
}
