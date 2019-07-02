using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trhvmgr.Database;

namespace trhvmgr
{
    /// <summary>
    /// Singleton class to manage this current session.
    /// </summary>
    public class SessionManager
    {
        #region Singleton pattern

        private static SessionManager instance;
        private SessionManager() { }

        public static SessionManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new SessionManager();
                return instance;
            }
        }

        #endregion

        #region Public Properties

        public DatabaseManager Database { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Disposes previous instance and creates a new instance of the database.
        /// </summary>
        public void InitializeDatabase()
        {
            Database?.Dispose();
            Database = null;
            Database = new DatabaseManager();
        }

        #endregion
    }
}
