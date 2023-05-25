using System.Net;

namespace Networking.Data
{
    public sealed class Host
    {
        public IPAddress Address { get; }
        public IPEndPoint Endpoint { get; }

        private const int _DEFAULT_PORT = 23456;

        public Host(string ip)
        {
            Address = IPAddress.Parse(ip);
            Endpoint = new IPEndPoint(Address, _DEFAULT_PORT);
        }
    }
}
