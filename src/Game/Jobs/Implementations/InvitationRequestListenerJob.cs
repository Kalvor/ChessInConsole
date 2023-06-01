using Game.Processes.Implementations;
using Game.Processes.Orchestration;
using Networking.Models;
using Networking.Services.Interfaces;

namespace Game.Jobs.Implementations
{
    public sealed class InvitationRequestListenerJob : BaseGameJob
    {
        private readonly INetworkAccessor _NetworkAccessor;
        public InvitationRequestListenerJob(JobsCancellationPool cancellationPool, INetworkAccessor networkAccessor) : base(cancellationPool)
        {
            _NetworkAccessor = networkAccessor;
        }

        public override async Task JobMethodAsync()
        {
            var invitation = await _NetworkAccessor.ListenFromDataAsync<GameInvitation>(this.CancellationToken);
            ProcessesOrchestrator.RedirectProcessControl<MainProcess, InvitationHandlingProcess>(invitation);
        }
    }
}
