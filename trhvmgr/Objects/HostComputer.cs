using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using trhvmgr.Database;

namespace trhvmgr.Objects
{
    public enum HostState
    {
        Unknown = 0,
        Online,
        Offline
    };

    public class HostComputer : ISerializableDbObject<DbHostComputer>
    {
        public string HostName { get; set; }
        public string MacAddress { get; set; }
        public string IpAddress { get; set; }
        public HostState State { get; set; }
        public List<Guid> VirtualMachines = new List<Guid>();
        public List<string> VirtualHardDisks = new List<string>();

        public DbHostComputer GetDbObject() => new DbHostComputer { HostName = HostName };

        [Obsolete("Use explicit casts instead")]
        public static HostComputer FromTreeNode(MasterTreeNode node) => (HostComputer)node;
        public static explicit operator HostComputer(MasterTreeNode node) => new HostComputer
        {
            HostName = node.Host,
            IpAddress = node.IpAddress,
            VirtualMachines = node.Children.Select(x => Guid.Parse(x.Uuid)).ToList()
        };
    }

    [Database("hosts")]
    public class DbHostComputer : IDbObject
    {
        [BsonId]
        public string HostName { get; set; }
    }
}
