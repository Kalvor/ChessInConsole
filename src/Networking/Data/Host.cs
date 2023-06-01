using System.Net;

namespace Networking.Data
{
    public sealed class Host
    {
        public IPAddress Address { get; }

        public Host(string ip)
        {
            Address = IPAddress.Parse(ip);
        }
    }
}
