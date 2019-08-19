using LiteDB;
using System;
using System.Collections.Generic;
using trhvmgr.Database;

namespace trhvmgr.Objects
{
    public class HostComputer : ISerializableDbObject<DbHostComputer>
    {
        public string HostName { get; set; }
        public string MacAddress { get; set; }
        public string IpAddress { get; set; }
        public List<Guid> VirtualMachines = new List<Guid>();
        public List<string> VirtualHardDisks = new List<string>();

        public DbHostComputer GetDbObject()
        {
            return new DbHostComputer { HostName = HostName };
        }
    }

    [Database("hosts")]
    public class DbHostComputer : IDbObject
    {
        [BsonId]
        public string HostName { get; set; }
    }
}
