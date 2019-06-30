using System;
using System.Net;
using System.Text.RegularExpressions;

namespace trhvmgr.Lib
{
    /// <summary>
    /// Collection of all network background workers
    /// for tasks such as getting the IP address etc...
    /// </summary>
    public class NetworkWorkers
    {
        /// <summary>
        /// First task that is run to pass down parameters (i.e., IP, HostName or MAC) to
        /// the worker chain.
        /// </summary>
        /// <param name="n">NetworkWorkerObject with proper parameter set.</param>
        /// <returns>Function delegate.</returns>
        public static Func<WorkerContext, WorkerContext> GetStarterWorker(NetworkWorkerObject n) => (ctx) =>
        {
            return new WorkerContext((int)StatusCode.OK, n, ctx.d);
        };

        /// <summary>
        /// IN:  Requires the HostName field to be filled.
        /// OUT: Will fill IpAddress field on success. Otherwise, IpAddress will be cleared to null.
        /// </summary>
        /// <returns>Function delegate.</returns>
        public static Func<WorkerContext, WorkerContext> GetIpWorker() => (ctx) =>
        {
            try
            {
                // If the previous did not return OK, fail.
                if (ctx.s != (int)StatusCode.OK) throw new Exception();
                // Get IP from hostname
                string server = ((NetworkWorkerObject)ctx.o).HostName;
                IPHostEntry hostEntry = Dns.GetHostEntry(server);
                ((NetworkWorkerObject)ctx.o).IpAddress = hostEntry.AddressList[0].ToString();
                ctx.s = (int)StatusCode.OK;
                return ctx;
            }
            catch (Exception)
            {
                // Set to null and fail
                ((NetworkWorkerObject)ctx.o).IpAddress = null;
                ctx.s = (int)StatusCode.FAILED;
                return ctx;
            }
        };

        /// <summary>
        /// IN:  Requires the IpAddress field to be filled.
        /// OUT: Will fill MacAddress field on success. Otherwise, MacAddress will be cleared to null.
        /// </summary>
        /// <returns>Function delegate.</returns>
        public static Func<WorkerContext, WorkerContext> GetMacWorker() => (ctx) =>
        {
            try
            {
                // If the previous did not return OK, fail.
                if (ctx.s != (int)StatusCode.OK) throw new Exception();
                // Get MAC Address
                IPAddress ip;
                IPAddress.TryParse(((NetworkWorkerObject)ctx.o).IpAddress, out ip);
                string mac = ArpRequest.Send(ip).Address.ToString();
                // Normalize address
                mac = Regex.Replace(mac, @"[^0-9A-Fa-f]", "");
                ((NetworkWorkerObject)ctx.o).MacAddress = mac;
                ctx.s = (int)StatusCode.OK;
                return ctx;
            }
            catch (Exception)
            {
                // Set to null and fail
                ((NetworkWorkerObject)ctx.o).MacAddress = null;
                ctx.s = (int)StatusCode.FAILED;
                return ctx;
            }
        };
    }
}