using Networking.Data;
using Networking.Models;
using Networking.Services.Interfaces;
using Newtonsoft.Json;
using System.Reflection;

namespace Networking.Services.Implementations
{
    internal sealed class NetworkDataReciever : INetworkDataReciever
    {
        private readonly INetworkAccessor _NetworkAccessor;
        public NetworkDataReciever(INetworkAccessor networkAccessor)
        {
            _NetworkAccessor = networkAccessor;
        }

        public async Task<GameInvitation> ListenForGameInvitationAsync()
        {
            var allPossibleInviters = _NetworkAccessor.GetAvaliableHosts().ToList();
            var invitationListeners = allPossibleInviters
                .Select(_NetworkAccessor.RecieveDataAsync)
                .ToList();
            await Task.WhenAny(invitationListeners);

            return JsonConvert.DeserializeObject<GameInvitation>(invitationListeners.First(c => c.IsCompleted).Result);
        }

        public async Task<GameInvitationResponse> ListenForGameInvitationResponseAsync(Host sender)
        {
            var data = await _NetworkAccessor.RecieveDataAsync(sender);
            return JsonConvert.DeserializeObject<GameInvitationResponse>(data);
        }

        public async Task<MoveInput> ListenForMoveAsync(Host sender)
        {
            var data = await _NetworkAccessor.RecieveDataAsync(sender);
            return JsonConvert.DeserializeObject<MoveInput>(data);
        }
    }
}
