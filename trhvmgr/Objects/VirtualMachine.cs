using LiteDB;
using System;
using System.Linq;
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

        [Obsolete("Use explicit casts instead")]
        public static VirtualMachine FromTreeNode(MasterTreeNode node) => (VirtualMachine) node;
        public static explicit operator VirtualMachine(MasterTreeNode node) => new VirtualMachine
        {
            Host = node.Host,
            Name = node.Name,
            Uuid = Guid.Parse(node.Uuid),
            Type = node.VmType == null ? VirtualMachineType.BASE : node.VmType.Value,
            VhdPath = node.Children.Select(x => x.Name).ToArray()
        };
    }

    [Database("vms")]
    public class DbVirtualMachine : IDbObject
    {
        public string Host { get; set; }
        [BsonId]
        public Guid Uuid { get; set; }
        public int VmType { get; set; }
        
        [Obsolete("Use explicit casts instead")]
        public static DbVirtualMachine FromTreeNode(MasterTreeNode node) => (DbVirtualMachine) node;
        public static explicit operator DbVirtualMachine(MasterTreeNode node) => new DbVirtualMachine
        {
            Host = node.Host,
            Uuid = Guid.Parse(node.Uuid),
            VmType = (int)(node.VmType == null ? VirtualMachineType.NONE : node.VmType.Value)
        };
    }
}
