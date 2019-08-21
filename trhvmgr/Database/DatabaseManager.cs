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
using System.Linq.Expressions;

namespace trhvmgr.Database
{
    public class DatabaseManager : IDisposable
    {
        public Dictionary<string, MasterTreeNode> Directory { get; private set; }

        private LiteDatabase _db;

        #region Constructor

        public DatabaseManager(string connectionString)
        {
            Directory = new Dictionary<string, MasterTreeNode>();
            _db = new LiteDatabase(connectionString);
        }

        public DatabaseManager() : this(ConfigurationManager.ConnectionStrings["ServerDB"].ConnectionString)
        {

        }

        #endregion

        #region Regenerative Methods

        /// <summary>
        /// Exception safe cache flushing for virtual machines.
        /// </summary>
        public void FlushCache()
        {
            foreach(var e in Directory) e.Value.BurnChildren();
            Directory.Clear();
            var hosts = GetCollection<DbHostComputer>();
            var virts = GetCollection<DbVirtualMachine>();
            // Loop through each host
            foreach (var h in hosts.FindAll())
            {
                MasterTreeNode root = null;
                HostState st = HostState.Unknown;
                var vm = new List<VirtualMachine>();
                try
                {
                    vm = Interface.GetVms(h.HostName);
                    st = HostState.Online;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK);
                    st = HostState.Offline;
                }
                root = this.GetRootTreeNode(h, st, vm);
                // Finally, add this host to our tree
                if(root != null) Directory.Add(h.HostName, root);
            }
        }

        private MasterTreeNode GetRootTreeNode(DbHostComputer dbHost, HostState state, List<VirtualMachine> cache)
        {
            var root = (MasterTreeNode) dbHost;
            root.State = new NodeState(state);
            foreach (var v in cache)
            {
                v.Type = GetVmDb(v.Uuid) == null ? VirtualMachineType.NONE : (VirtualMachineType) GetVmDb(v.Uuid).VmType;
                // First, add all virtual machines associated with this host
                var vnode = (MasterTreeNode) v;
                // Then, add all virtual hard disks associated with this host
                foreach (var vhd in v.VhdPath)
                    vnode.Children.Add(MasterTreeNode.GetTreeNode(vhd, v.Host, NodeType.VirtualHardDisks));
                root.Children.Add(vnode);
            }
            return root;
        }

        #endregion

        #region Add/Get/Set Server/VM Methods

        /// <summary>
        /// Adds a server to the database. If a cache of virtual machines is given,
        /// then we insert the virtual machines into the Master Tree. If not, then we
        /// obtain a list of virtual machines and then insert it into the Master Tree.
        /// This function is cache safe.
        /// </summary>
        /// <param name="dbHost">The computer object to insert into the database.</param>
        /// <param name="cache">An optional cache parameter.</param>
        public void AddServer(DbHostComputer dbHost, HostState state, List<VirtualMachine> cache = null)
        {
            Insert(dbHost, x => x.HostName);
            if (cache == null)
                cache = Interface.GetVms(dbHost.HostName);
            var root = this.GetRootTreeNode(dbHost, state, cache);
            Directory.Add(dbHost.HostName, root);
        }

        /// <summary>
        /// Removes a server from the database.
        /// This function is cache safe.
        /// </summary>
        public void RemoveServer(string host)
        {
            if (!Directory.ContainsKey(host)) return;
            Delete<DbHostComputer, string>(x => x.HostName == host, x => x.HostName);
            Directory[host].BurnChildren();
            Directory.Remove(host);
        }

        /*public List<HostComputer> GetServers()
        {
            return Directory.Values.Select(x => (HostComputer) x).ToList();
        }*/

        public List<DbHostComputer> GetServerDb()
        {
            List<DbHostComputer> ret = new List<DbHostComputer>();
            var hosts = GetCollection<DbHostComputer>();
            return hosts.FindAll().ToList();
        }

        public void SetVmType(DbVirtualMachine vm, VirtualMachineType type)
        {
            vm.VmType = (int) type;
            // !! It is important to normalize the strings with ToUpper()
            var node = Directory[vm.Host].Children.Find(x => x.Uuid.ToUpper() == vm.Uuid.ToString().ToUpper());
            node.VmType = new NodeVirtualMachineType(type);
            Update(vm, x => x.Uuid);
        }

        public DbVirtualMachine GetVmDb(Guid id)
        {
            var vms = GetCollection<DbVirtualMachine>();
            var col = vms.Find(x => x.Uuid == id);
            if (col.Count() > 0)
                return col.ElementAt(0);
            return null;
        }

        public List<VirtualMachine> GetVm(VirtualMachineType type)
        {
            List<VirtualMachine> res = new List<VirtualMachine>();
            foreach (var c in Directory.Values)
                c.Children.Where(x => x.VmType?.Value == type).ToList().ForEach(x => res.Add((VirtualMachine) x));
            return res;
        }

        public List<VirtualMachine> GetVm(string host)
        {
            List<VirtualMachine> res = new List<VirtualMachine>();
            Directory[host].Children.ForEach(x => res.Add((VirtualMachine) x));
            return res;
        }

        public VirtualMachine GetVm(string host, Guid id)
        {
            VirtualMachine vm = null;
            Directory[host].Children.ForEach(x =>
            {
                if (Guid.Parse(x.Uuid) == id)
                    vm = (VirtualMachine)x;
            });
            return vm;
        }

        #endregion

        #region Exposed DB Methods

        public LiteCollection<BsonDocument> GetCollection(string name) => _db.GetCollection(name);

        /// <summary>
        /// Gets database collection from typename
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        public LiteCollection<T> GetCollection<T>() where T : IDbObject
        {
            string collectionName = null;
            foreach (var a in typeof(T).GetCustomAttributes(true))
            {
                if (a is DatabaseAttribute)
                {
                    collectionName = (a as DatabaseAttribute).CollectionName;
                    break;
                }
            }
            if (string.IsNullOrEmpty(collectionName))
                throw new InvalidOperationException("Invalid collection type: " + typeof(T).Assembly.FullName + ". Are you missing the DatabaseAttribute?");
            return _db.GetCollection<T>(collectionName);
        }

        /// <summary>
        /// Updates an object in the database. If the object is not found, it is then
        /// inserted into the database.
        /// This function is not cache safe.
        /// </summary>
        /// <typeparam name="T">Type of object to insert</typeparam>
        /// <typeparam name="K">Type of field to ensure index</typeparam>
        /// <param name="o">Object to update</param>
        /// <param name="property">Ensure index expression</param>
        public void Update<T, K>(T o, Expression<Func<T, K>> property) where T : IDbObject
        {
            var col = GetCollection<T>();
            col.EnsureIndex(property);
            if (!col.Update(o))
                col.Insert(o);
        }

        /// <summary>
        /// Updates an object in the database. If the object is not found, it is then
        /// inserted into the database.
        /// This function is not cache safe.
        /// </summary>
        /// <typeparam name="T">Type of object to insert</typeparam>
        /// <typeparam name="K">Type of field to ensure index</typeparam>
        /// <param name="o">Collection of object to update</param>
        /// <param name="property">Ensure index expression</param>
        public void Update<T, K>(IEnumerable<T> o, Expression<Func<T, K>> property) where T : IDbObject
        {
            foreach (T e in o)
                Update(e, property);
        }

        public void Insert<T, K>(T o, Expression<Func<T, K>> property) where T : IDbObject
        {
            var col = GetCollection<T>();
            col.EnsureIndex(property);
            col.Insert(o);
        }

        public void Insert<T, K>(IEnumerable<T> o, Expression<Func<T, K>> property) where T : IDbObject
        {
            var col = GetCollection<T>();
            col.EnsureIndex(property);
            col.Insert(o);
        }

        public void Delete<T, K>(Expression<Func<T, bool>> predicate, Expression<Func<T, K>> property) where T : IDbObject
        {
            var col = GetCollection<T>();
            col.EnsureIndex(property);
            col.Delete(predicate);
        }

        public IEnumerable<string> GetCollectionNames() => _db.GetCollectionNames();

        public IList<BsonValue> EngineRun(string cmd) => _db.Engine.Run(cmd);

        protected virtual void Dispose(bool s)
        {
            _db.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
