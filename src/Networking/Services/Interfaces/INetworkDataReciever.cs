﻿using Networking.Data;
using Networking.Models;

namespace Networking.Services.Interfaces
{
    public interface INetworkDataReciever
    {
        Task<GameInvitation> ListenForGameInvitationAsync();
        Task<MoveInput> ListenForMoveAsync(Host sender);
    }
}
