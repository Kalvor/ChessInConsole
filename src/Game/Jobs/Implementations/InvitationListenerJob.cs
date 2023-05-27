using System.Diagnostics;

namespace Game.Jobs.Implementations
{
    public sealed class InvitationListenerJob : BaseGameJob
    {
        public InvitationListenerJob(CancellationPool cancellationPool) : base(cancellationPool)
        {
        }

        public override async Task RunAsync()
        {
            await base.HostRun(internalRunAsync);
        }

        private async Task internalRunAsync()
        {
            Thread.Sleep(1000);
 
            Console.WriteLine("InvListener");
    
        }
    }
}
