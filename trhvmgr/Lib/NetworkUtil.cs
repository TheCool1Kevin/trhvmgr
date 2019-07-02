using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace trhvmgr.Lib
{
    /// <summary>
    /// Utility class for network related methods.
    /// </summary>
    public class NetworkUtil
    {
        /// <summary>
        /// Sends a ping packet/multiple ping packets. Returns success/last status.
        /// </summary>
        /// <param name="ip">IP Address to send ping to.</param>
        /// <param name="tries">Number of tries before giving up.</param>
        /// <param name="timeout">Timeout of each response.</param>
        /// <returns>
        /// If server replied with success within the number of tries specified, return success.
        /// Otherwise, we return the very last status received.
        /// </returns>
        public static IPStatus SendPing(IPAddress ip, int tries = 1, int timeout = 1000 /* 1 second */)
        {
            Ping ping = new Ping();
            IPStatus reply = IPStatus.Unknown;
            for (int i = 0; i < tries; i++)
            {
                reply = ping.Send(ip, timeout).Status;
                if (reply == IPStatus.Success) break;
            }
            return reply;
        }
    }
}
