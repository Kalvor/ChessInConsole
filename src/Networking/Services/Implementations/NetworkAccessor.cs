using Networking.Data;
using Networking.Services.Interfaces;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Networking.Services.Implementations
{
    public sealed class NetworkAccessor : INetworkAccessor
    {
        public IEnumerable<Host> GetAvaliableHosts()
        {
            return GetARPResult()
                .Split(new char[] { '\n', '\r' })
                .Where(c => !String.IsNullOrEmpty(c))
                .Select(c => c.Split(new char[] { ' ', '\t' }))
                .Where(c => c.Length == 3)
                .Select(c => new Host(c[0]));
        }

        public async Task<string> RecieveDataAsync(Host sender)
        {
            bool isConnected = false;

            using var socket = SetupSocket();
            while (!isConnected)
            {
                try
                {
                    await socket.ConnectAsync(sender.Endpoint);
                    isConnected = true;
                }
                catch (SocketException) { await Task.Delay(200); }
                catch (Exception) { throw; }
            }
            ArraySegment<byte> buffer = new byte[socket.ReceiveBufferSize];
            await socket.ReceiveAsync(buffer);
            socket.Close();
            return Encoding.ASCII.GetString(buffer);
        }

        public async Task SendDataAsync(Host reciever, string jsonMessage)
        {
            using var socket = SetupSocket();
            socket.Bind(new IPEndPoint(IPAddress.Any, 23456));
            socket.Listen(100);
            using var client = await socket.AcceptAsync();
            byte[] encodedMessage = !client.RemoteEndPoint!.ToString()!.StartsWith(reciever.Address.ToString())
                ? Encoding.ASCII.GetBytes("ERROR : Currently host is not intending to send data to you.")
                : Encoding.ASCII.GetBytes(jsonMessage);

            await client.SendToAsync(encodedMessage, reciever.Endpoint);
            
            client.Close();
            socket.Close();
        }

        private string GetARPResult()
        {
            Process p = null;
            string output = string.Empty;

            try
            {
                p = Process.Start(new ProcessStartInfo("arp", "-a")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                });

                output = p.StandardOutput.ReadToEnd();

                p.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("IPInfo: Error Retrieving 'arp -a' Results", ex);
            }
            finally
            {
                if (p != null)
                {
                    p.Close();
                }
            }

            return output;
        }
        private Socket SetupSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                ReceiveTimeout = int.MaxValue,
                SendTimeout = int.MaxValue
            };
        }
    }
}
