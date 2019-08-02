using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using trhvmgr.Objects;
using trhvmgr.Plugs;
using System.Windows.Forms;

namespace trhvmgr.Database
{
    public class DatabaseManager : IDisposable
    {
        public List<MasterTreeNode> TreeNodes { get; private set; }

        private LiteDatabase _db;

        #region Constructor

        public DatabaseManager(string connectionString)
        {
            TreeNodes = new List<MasterTreeNode>();
            _db = new LiteDatabase(connectionString);
            RegenerateTree();
        }

        public DatabaseManager() : this(ConfigurationManager.ConnectionStrings["ServerDB"].ConnectionString)
        {

        }

        #endregion

        #region Public Methods

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
                try
                {
                    var p = Interface.GetVms(h.HostName);
                    foreach (var v in p)
                    {
                        var vnode = new MasterTreeNode
                        {
                            Name = v.Name,
                            Host = v.Host,
                            Uuid = v.Uuid.ToString().ToUpper(),
                            Type = NodeType.VirtualMachines,
                        };
                        foreach (var vhd in v.VhdPath)
                        {
                            vnode.Children.Add(new MasterTreeNode
                            {
                                Name = vhd,
                                Host = v.Host,
                                Type = NodeType.VirtualHardDisks
                            });
                        }
                        root.Children.Add(vnode);
                    }
                } catch(Exception e)
                {
                    MessageBox.Show(e.Message, "Exception", MessageBoxButtons.OK);
                    return;
                }
                
                // Then, add all virtual hard disks associated with this host


                // Finally, add this host to our tree
                TreeNodes.Add(root);
            }
        }

        #endregion

        #region Add/Get/Set Server/VM Commands

        public void AddServer(DbHostComputer dbHost)
        {
            var hosts = _db.GetCollection<DbHostComputer>("hosts");
            hosts.Insert(dbHost);
            hosts.EnsureIndex(x => x.HostName);
            RegenerateTree();
        }

        public List<DbHostComputer> GetServers()
        {
            List<DbHostComputer> ret = new List<DbHostComputer>();
            var hosts = _db.GetCollection<DbHostComputer>("hosts");
            return hosts.FindAll().ToList();
        }

        #endregion

        #region Exposed DB Methods

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
