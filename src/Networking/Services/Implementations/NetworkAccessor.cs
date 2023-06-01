﻿using Networking.Data;
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

        public async Task<TData> ListenFromDataAsync<TData>(Host sender, CancellationToken ct)
        {
            while (ct.IsCancellationRequested)
            {
                var connectedSocket = await GetSocketConnectionAsync(ct);
                try
                {
                    if(((IPEndPoint)connectedSocket.RemoteEndPoint!).Address != sender.Address)
                    {
                        continue;
                    }
                    byte[] data = new byte[1024];
                    await connectedSocket.ReceiveAsync(data, ct);
                    connectedSocket.Close();
                    var dataString = Encoding.ASCII.GetString(data);
                    TData result = JsonConvert.DeserializeObject<TData>(dataString);
                    return result;
                }
                catch (Exception) { continue; }
            }
            throw new Exception();
        }

        public async Task<TData> ListenFromDataAsync<TData>(CancellationToken ct)
        {
            while (ct.IsCancellationRequested) 
            {
                var connectedSocket = await GetSocketConnectionAsync(ct);
                try
                {

                    byte[] data = new byte[1024];
                    await connectedSocket.ReceiveAsync(data, ct);
                    connectedSocket.Close();
                    var dataString = Encoding.ASCII.GetString(data);
                    TData result = JsonConvert.DeserializeObject<TData>(dataString);
                    return result;
                }
                catch (Exception) { continue; }
            }
            throw new Exception();
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

        private async Task<Socket> GetSocketConnectionAsync(CancellationToken ct)
        {
            TcpListener listener = new TcpListener(GetLocalHost().Address, 8001);
            listener.ExclusiveAddressUse = true;
            listener.Start();
            Socket s = await listener.AcceptSocketAsync(ct);
            listener.Stop();
            return s;
        }
    }
}
