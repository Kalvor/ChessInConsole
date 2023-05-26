using Networking.Data;
using Networking.Models;
using Networking.Services.Interfaces;
using Newtonsoft.Json;

namespace Networking.Services.Implementations
{
    internal sealed class NetworkDataSender : INetworkDataSender
    {
        private readonly INetworkAccessor _NetworkAccessor;
        public NetworkDataSender(INetworkAccessor networkAccessor)
        {
            _NetworkAccessor = networkAccessor;
        }

        public async Task SendGameInvitationAsync(GameInvitation invitation, Host reciever)
        {
            invitation.InvitorHost = _NetworkAccessor.GetLocalHost();
            await _NetworkAccessor.SendDataAsync(reciever,JsonConvert.SerializeObject(invitation));
        }

        public async Task SendMoveAsync(MoveInput move, Host reciever)
        {
            await _NetworkAccessor.SendDataAsync(reciever, JsonConvert.SerializeObject(move));

        }
    }
}
