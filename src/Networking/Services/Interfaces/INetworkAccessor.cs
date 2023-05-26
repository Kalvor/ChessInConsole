using Networking.Data;

namespace Networking.Services.Interfaces
{
    public interface INetworkAccessor
    {
        IEnumerable<Host> GetAvaliableHosts();
        Host GetLocalHost();
        Task SendDataAsync(Host reciever, string jsonMessage);
        Task<string> RecieveDataAsync(Host sender);
    }
}
 