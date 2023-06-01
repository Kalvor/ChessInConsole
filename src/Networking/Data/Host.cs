using Newtonsoft.Json;
using System.Net;

namespace Networking.Data
{
    public sealed class Host
    {
        [JsonIgnore]
        public IPAddress Address => IPAddress.Parse(Ip);
        public string Ip { get; }

        public Host(string ip)
        {
            Ip = ip;
        }
    }
}
