using Networking.Data;
using Networking.Models;

namespace Networking.Services.Interfaces
{
    public interface INetworkDataSender
    {
        Task SendGameInvitationAsync(GameInvitation invitation, Host reciever);
        Task SendGameInvitationResponseAsync(GameInvitationResponse response, Host reciever);
        Task SendMoveAsync(MoveInput move, Host reciever);
    }
}
