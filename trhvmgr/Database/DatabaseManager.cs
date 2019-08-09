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
        public List<MasterTreeNode> TreeNodes { get; private set; }

        private LiteDatabase _db;

        #region Constructor

        public DatabaseManager(string connectionString)
        {
            TreeNodes = new List<MasterTreeNode>();
            _db = new LiteDatabase(connectionString);
        }

        public DatabaseManager() : this(ConfigurationManager.ConnectionStrings["ServerDB"].ConnectionString)
        {

        }

        #endregion

        #region Regenerative Methods

        public void RegenerateTree()
        {
            TreeNodes.Clear();
            var hosts = GetCollection<DbHostComputer>();
            var virts = GetCollection<DbVirtualMachine>();
            // Loop through each host
            foreach (var h in hosts.FindAll())
            {
                MasterTreeNode root = null;
                var vm = new List<VirtualMachine>();
                try
                {
                    vm = Interface.GetVms(h.HostName);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK);
                }
                root = this.GetRootTreeNode(h, vm);
                // Finally, add this host to our tree
                if(root != null) TreeNodes.Add(root);
            }
        }

        private MasterTreeNode GetRootTreeNode(DbHostComputer dbHost, List<VirtualMachine> cache)
        {
            var root = MasterTreeNode.GetTreeNode(dbHost);
            foreach (var v in cache)
            {
                // First, add all virtual machines associated with this host
                var vnode = MasterTreeNode.GetTreeNode(v);
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
        public void AddServer(DbHostComputer dbHost, List<VirtualMachine> cache = null)
        {
            Insert(dbHost, x => x.HostName);
            if (cache == null)
                cache = Interface.GetVms(dbHost.HostName);
            var root = this.GetRootTreeNode(dbHost, cache);
            TreeNodes.Add(root);
        }

        /// <summary>
        /// Removes a server from the database.
        /// This function is cache safe.
        /// </summary>
        /// <param name="host">The unique hostname of the computer to remove.</param>
        public void RemoveServer(string host)
        {
            Delete<DbHostComputer, string>(x => x.HostName == host, x => x.HostName);
            TreeNodes.Find(x => x.Name == host).BurnChildren();
            TreeNodes.Remove(TreeNodes.Find(x => x.Name == host));
        }

        /// <summary>
        /// Obtains a list of all servers from the database.
        /// </summary>
        public List<DbHostComputer> GetServers()
        {
            List<DbHostComputer> ret = new List<DbHostComputer>();
            var hosts = _db.GetCollection<DbHostComputer>("hosts");
            return hosts.FindAll().ToList();
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
            col.Update(o);
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
            var col = GetCollection<T>();
            col.EnsureIndex(property);
            col.Update(o);
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
