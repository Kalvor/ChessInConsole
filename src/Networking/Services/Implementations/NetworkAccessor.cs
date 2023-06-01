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

        public async Task<TData?> ListenFromDataAsync<TData>(Host sender)
        {
            while (true)
            {
                var connectedSocket = await GetSocketConnectionAsync();
                try
                {
                    if(!((IPEndPoint)connectedSocket.RemoteEndPoint!).Address.ToString().StartsWith(sender.Address.ToString()))
                    {
                        continue;
                    }
                    byte[] data = new byte[1024];
                    await connectedSocket.ReceiveAsync(data);
                    connectedSocket.Close();
                    var dataString = Encoding.ASCII.GetString(data);
                    TData result = JsonConvert.DeserializeObject<TData>(dataString);

                    if (result == null) continue;

                    return result;
                }
                catch (Exception) 
                { 
                    continue; 
                }
            }
        }

        public async Task<TData?> ListenFromDataAsync<TData>()
        {
            while (true) 
            {
                var connectedSocket = await GetSocketConnectionAsync();
                try
                {

                    byte[] data = new byte[1024];
                    await connectedSocket.ReceiveAsync(data);
                    connectedSocket.Close();
                    var dataString = Encoding.ASCII.GetString(data);
                    TData result = JsonConvert.DeserializeObject<TData>(dataString);

                    if (result == null) continue;

                    return result;
                }
                catch(Exception) 
                {
                    continue; 
                }
            }
        }

        public async Task SendDataAsync(Host reciever, string jsonMessage)
        {
            TcpClient client = new TcpClient();
            client.SendTimeout = 1000;
            await client.ConnectAsync(reciever.Address, 8001);
            using Stream dataStream = client.GetStream();
            byte[] data = Encoding.ASCII.GetBytes(jsonMessage);
            await dataStream.WriteAsync(data, 0, data.Length);
            client.Close();
        }

        private async Task<Socket> GetSocketConnectionAsync()
        {
            TcpListener listener = new TcpListener(GetLocalHost().Address, 8001);
            listener.Start();
            Socket s = await listener.AcceptSocketAsync();
            listener.Stop();
            return s;
        }
    }
}
