using Networking.Data;
using Networking.Services.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace Networking.Services.Implementations
{
    internal sealed class NetworkAccessor : INetworkAccessor
    {
        public Host GetLocalHost() //TODO
        {
            return new Host(NetworkInterface.GetAllNetworkInterfaces()
                .Where(c => c.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || c.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                .Where(c => c.Name.ToLower().StartsWith("wi-fi"))
                .Select(c => c.GetIPProperties().UnicastAddresses.Where(a => a.Address.AddressFamily == AddressFamily.InterNetwork).First())
                .First().Address.ToString());
        }

        public async Task<byte[]> ListenFromData(CancellationToken ct)
        {
            TcpListener listener = new TcpListener(GetLocalHost().Address, 8001);
            listener.ExclusiveAddressUse = true;
            listener.Start();
            Socket s = await listener.AcceptSocketAsync(ct);
            byte[] data = new byte[1024];
            await s.ReceiveAsync(data,ct);
            s.Close();
            listener.Stop();
            return data;
        }

        public async Task SendDataAsync(Host reciever, string jsonMessage, CancellationToken ct)
        {
            TcpClient client = new TcpClient();
            await client.ConnectAsync(reciever.Address, 8001,ct);
            using Stream dataStream = client.GetStream();
            byte[] data = Encoding.ASCII.GetBytes(jsonMessage);
            await dataStream.WriteAsync(data, 0, data.Length,ct);
            client.Close();
        }
    }
}
