using LiteDB;
using System;
using System.Linq;
using System.Management.Automation;
using trhvmgr.Database;
using trhvmgr.Lib;

namespace trhvmgr.Objects
{
    public enum VirtualMachineType
    {
        NONE = 0,
        BASE,
        TEMPLATE,
        DEPLOY
    };

    public enum VirtualMachineState
    {
        Unknown = 0,
        Running,
        Off,
        Paused,
        Other
    };

    public class VirtualMachine : ISerializableDbObject<DbVirtualMachine>
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public string[] VhdPath { get; set; }
        public Guid Uuid { get; set; }
        public VirtualMachineType Type { get; set; }
        public VirtualMachineState State { get; set; }

        public string ParentHost { get; set; }
        public Guid ParentUuid { get; set; }

        public DbVirtualMachine GetDbObject()
        {
            return new DbVirtualMachine
            {
                Host = Host,
                Uuid = Uuid,
                ParentHost = ParentHost,
                ParentUuid = ParentUuid,
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
            Type = node.VmType == null ? VirtualMachineType.NONE : node.VmType.Value,
            VhdPath = node.Children.Where(x => x.Type == NodeType.VirtualHardDisks).Select(x => x.Name).ToArray(),
            ParentHost = node.ParentHost,
            ParentUuid = node.ParentUuid
        };

        public static VirtualMachineState GetStateFromString(string st)
        {
            switch(st.ToLowerInvariant())
            {
                case "": return VirtualMachineState.Unknown;
                case "running": return VirtualMachineState.Running;
                case "off": return VirtualMachineState.Off;
                case "paused": return VirtualMachineState.Paused;
                default: return VirtualMachineState.Other;
            }
        }

        public static VirtualMachine FromPSObject(PSObject m, string hostName)
        {
            return new VirtualMachine
            {
                Host = hostName,
                Name = m.Members["VMName"].Value.ToString(),
                Uuid = (Guid)m.Members["VMId"].Value,
                State = VirtualMachine.GetStateFromString(m.Members["State"].Value.ToString()),
                VhdPath = Array.ConvertAll(PSWrapper.Execute(hostName, (ps) =>
                {
                    return ps.AddCommand("Get-VM")
                        .AddParameter("Id", m.Members["VMId"].Value)
                        .AddCommand("Get-VMHardDiskDrive")
                        .Invoke();
                }).ToArray(), (x) => { return x.Members["Path"].Value.ToString(); }),
                Type = VirtualMachineType.NONE
            };
        }
    }

    [Database("vms")]
    public class DbVirtualMachine : IDbObject
    {
        public string Host { get; set; }
        [BsonId]
        public Guid Uuid { get; set; }

        public string ParentHost { get; set; }
        public Guid ParentUuid { get; set; }

        public int VmType { get; set; }
        
        [Obsolete("Use explicit casts instead")]
        public static DbVirtualMachine FromTreeNode(MasterTreeNode node) => (DbVirtualMachine) node;
        [Obsolete("DO NOT USE THIS CAST, YOU FOOL!")]
        public static explicit operator DbVirtualMachine(MasterTreeNode node) => new DbVirtualMachine
        {
            Host = node.Host,
            Uuid = Guid.Parse(node.Uuid),
            VmType = (int)(node.VmType == null ? VirtualMachineType.NONE : node.VmType.Value)
        };
    }
}
