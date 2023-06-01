using Networking.Data;

namespace Networking.Services.Interfaces
{
    public interface INetworkAccessor
    {
        Host GetLocalHost();
        Task SendDataAsync(Host reciever, string jsonMessage);
        Task<TData?> ListenFromDataAsync<TData>(Host sender);
        Task<TData?> ListenFromDataAsync<TData>();
    }
}
 