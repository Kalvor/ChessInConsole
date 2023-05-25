using Networking.Data;

namespace Networking.Services.Interfaces
{
    public interface INetworkAccessor
    {
        IEnumerable<Host> GetAvaliableHosts();
        Task<bool> SendDataAsync(Host reciever, string jsonMessage);
        Task<string> RecieveDataAsync(Host sender);
    }
}
