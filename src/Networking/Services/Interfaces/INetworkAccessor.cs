using Networking.Data;

namespace Networking.Services.Interfaces
{
    public interface INetworkAccessor
    {
        Host GetLocalHost();
        Task SendDataAsync(Host reciever, string jsonMessage, CancellationToken ct);
        Task<TData?> ListenFromDataAsync<TData>(Host sender, CancellationToken ct);
        Task<TData?> ListenFromDataAsync<TData>(CancellationToken ct);
    }
}
 