using Networking.Models;
using Networking.Services.Interfaces;

namespace Game.Jobs.Implementations
{
    public sealed class InvitationRequestListenerJob : BaseGameJob
    {
        private GameInvitation _GameInvitation { get; set; }
        public InvitationRequestListenerJob(JobsCancellationPool cancellationPool) : base(cancellationPool)
        {
        }

        public override async Task JobMethodAsync()
        {
            _GameInvitation = new GameInvitation();

            //var invitation = await _NetDataReciever.ListenForGameInvitationAsync();

            Thread.Sleep(5000);
            //Console.WriteLine("Mam zapro");
        }
    }
}
