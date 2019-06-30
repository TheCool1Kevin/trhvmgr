using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace trhvmgr.Lib
{
    /// <summary>
    /// Object passed down a chain of network workers.
    /// </summary>
    public class NetworkWorkerObject : ICloneable
    {
        public string HostName { get; set; }
        public string IpAddress { get; set; }
        public string MacAddress { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
