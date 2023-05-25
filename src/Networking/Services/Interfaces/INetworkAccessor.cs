using Networking.Data;

namespace Networking.Services.Interfaces
{
    public interface INetworkAccessor
    {
        IEnumerable<Host> GetAvaliableHosts();
        Task<bool> SendDataToHostAsync(Host reciever, string jsonMessage);
        Task<string> ListenForMessageAsync();
    }
}
