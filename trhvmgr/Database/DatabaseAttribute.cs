using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trhvmgr.Database
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DatabaseAttribute : Attribute
    {
        public string CollectionName { get; set; }
        
        public DatabaseAttribute(string CollectionName)
        {
            this.CollectionName = CollectionName;
        }
    }
}
