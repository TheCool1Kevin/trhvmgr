using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using trhvmgr.Objects;
using trhvmgr.Plugs;

namespace trhvmgr.Database
{
    public class DatabaseManager : IDisposable
    {
        public List<MasterTreeNode> TreeNodes { get; private set; }
        private LiteDatabase _db;

        public DatabaseManager()
        {
            TreeNodes = new List<MasterTreeNode>();
            _db = new LiteDatabase(ConfigurationManager.ConnectionStrings["ServerDB"].ConnectionString);
            RegenerateTree();
        }

        #region Public Methods

        public void AddServer(DbHostComputer dbHost)
        {
            var hosts = _db.GetCollection<DbHostComputer>("hosts");
            hosts.Insert(dbHost);
            hosts.EnsureIndex(x => x.HostName);
            RegenerateTree();
        }

        public void RegenerateTree()
        {
            TreeNodes.Clear();
            var hosts = _db.GetCollection<DbHostComputer>("hosts");
            var virts = _db.GetCollection<DbVirtualMachine>("vms");
            hosts.EnsureIndex(x => x.HostName);
            virts.EnsureIndex(x => x.Uuid);
            // Loop through each host
            foreach (var h in hosts.FindAll())
            {
                var root = new MasterTreeNode
                {
                    Name = h.HostName,
                    Type = NodeType.HostComputer
                };
                // First, add all virtual machines associated with this host
                foreach (var v in Interface.GetVms(h.HostName))
                {
                    var vm = virts.FindOne(x => x.Uuid == v.Uuid);
                    root.Children.Add(new MasterTreeNode
                    {
                        Name = v.Name,
                        Host = v.Host,
                        Uuid = v.Uuid.ToString(),
                        Type = NodeType.VirtualMachines
                    });
                }
                // Then, add all virtual hard disks associated with this host

                // Finally, add this host to our tree
                TreeNodes.Add(root);
            }
        }

        #endregion

        #region Exposed Methods

        public IEnumerable<string> GetCollectionNames() => _db.GetCollectionNames();

        public LiteCollection<BsonDocument> GetCollection(string name) => _db.GetCollection(name);

        public IList<BsonValue> EngineRun(string cmd) => _db.Engine.Run(cmd);

        public void Dispose()
        {
            _db.Dispose();
        }

        #endregion
    }
}
