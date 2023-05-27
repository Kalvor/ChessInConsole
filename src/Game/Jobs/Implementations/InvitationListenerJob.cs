using Networking.Services.Interfaces;

namespace Game.Jobs.Implementations
{
    public sealed class InvitationListenerJob : BaseGameJob
    {
        private readonly INetworkDataReciever _NetDataReciever;
        public InvitationListenerJob(JobsCancellationPool cancellationPool, INetworkDataReciever netDataReciever) : base(cancellationPool)
        {
            _NetDataReciever = netDataReciever;
        }

        public override async Task JobMethodAsync()
        {
            var invitation = await _NetDataReciever.ListenForGameInvitationAsync();

            Console.WriteLine("Mam zapro");
        }
    }
}
