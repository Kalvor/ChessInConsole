using Networking.Data;
using Networking.Services.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
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

        public async Task<string> ListenForMessageAsync()
        {

            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Connect(new IPEndPoint(IPAddress.Loopback, 23456));
                while (true)
                {
                    byte[] buffer= new byte[1024];
                    var read = socket.Receive(buffer);
                    string msg = Encoding.ASCII.GetString(buffer);
                }
            }
            return "";
        }

        public async Task<bool> SendDataToHostAsync(Host reciever, string jsonMessage)
        {
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Bind(new IPEndPoint(IPAddress.Any, 23456));
                socket.Listen(100);
                while (true)
                {
                    using (var client = socket.Accept())
                    {
                        client.Send(Encoding.ASCII.GetBytes("DUPA"));
                    }
                }
            }
            return false;
        }
    }
}
