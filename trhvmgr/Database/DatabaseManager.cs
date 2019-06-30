using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using trhvmgr.Objects;
using System.Windows.Forms;

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

        public void RegenerateTree()
        {
            TreeNodes.Clear();
            var hosts = _db.GetCollection<HostComputer>("hosts");
            var virts = _db.GetCollection<VirtualMachine>("vms");
            hosts.EnsureIndex(x => x.HostName);
            virts.EnsureIndex(x => x.UUID);
            // Loop through each host
            foreach (var h in hosts.FindAll())
            {
                var root = new MasterTreeNode { Name = h.HostName };
                // First, add all virtual machines associated with this host
                foreach (var v in h.VMs)
                {
                    var vm = virts.FindOne(x => x.UUID == v);
                    root.Children.Add(new MasterTreeNode
                    {
                        Name = vm.Name,
                        Host = vm.Host,
                        UUID = vm.UUID
                    });
                }
                // Then, add all virtual hard disks associated with this host

                // Finally, add this host to our tree
                TreeNodes.Add(root);
            }
        }

        public IEnumerable<string> GetCollectionNames() => _db.GetCollectionNames();

        public LiteCollection<BsonDocument> GetCollection(string name) => _db.GetCollection(name);

        public IList<BsonValue> EngineRun(string cmd) => _db.Engine.Run(cmd);

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
