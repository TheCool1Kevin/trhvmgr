using System.Linq;
using System.Net.NetworkInformation;

namespace trhvmgr.Lib
{
    public class NetworkUtil
    {
        public static string GetMacByIP(string ipAddress)
        {
            // Grab all online interfaces
            var query = NetworkInterface.GetAllNetworkInterfaces()
                .Where(n =>
                    n.OperationalStatus == OperationalStatus.Up && // Only grabbing what's online
                    n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(_ => new
                {
                    PhysicalAddress = _.GetPhysicalAddress(),
                    IPProperties = _.GetIPProperties(),
                });

            // Grab the first interface that has a unicast address that matches your search string
            var mac = query
                .Where(q => q.IPProperties.UnicastAddresses
                    .Any(ua => ua.Address.ToString() == ipAddress))
                .FirstOrDefault()
                .PhysicalAddress;

            // Return the mac address with formatting (eg "00-00-00-00-00-00")
            return string.Join("-", mac.GetAddressBytes().Select(b => b.ToString("X2")));
        }
    }
}
