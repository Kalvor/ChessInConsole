using Networking.Data;
using Networking.Services.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime;
using System.Text;

namespace Networking.Services.Implementations
{
    public sealed class NetworkAccessor : INetworkAccessor
    {
        const int PORT_NO = 5000;
        private static string GetARPResult()
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

        public IEnumerable<Host> GetAvaliableHosts()
        {

            foreach (var arp in GetARPResult().Split(new char[] { '\n', '\r' }))
            {
                // Parse out all the MAC / IP Address combinations
                if (!string.IsNullOrEmpty(arp))
                {
                    var pieces = (from piece in arp.Split(new char[] { ' ', '\t' })
                                  where !string.IsNullOrEmpty(piece)
                                  select piece).ToArray();
                    if (pieces.Length == 3)
                    {
                        Console.WriteLine(pieces[1] + " || " + pieces[0]); 
                    }
                }
            }
            return null;
        }

        public async Task<string> RecieveDataAsync(Host sender)
        {
            var msg = string.Empty;
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Connect(new IPEndPoint(sender.Address, 23456));
                bool flag = true;
                while (flag)
                {
                    byte[] buffer= new byte[socket.ReceiveBufferSize];
                    var read = socket.Receive(buffer);
                    msg = Encoding.ASCII.GetString(buffer);
                    flag = false;
                }
            }
            return msg;
        }

        public async Task<bool> SendDataAsync(Host reciever, string jsonMessage)
        {
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Bind(new IPEndPoint(IPAddress.Any, 23456));
                socket.Listen(100);
                bool flag = true;
                while (flag)
                {
                    using (var client = socket.Accept())
                    {
                        client.SendTo(Encoding.ASCII.GetBytes(jsonMessage), new IPEndPoint(reciever.Address, 23456));
                        flag = false;
                    }
                }
                return true;
            }
        }
    }
}
