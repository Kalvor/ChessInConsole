using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Threads.GameThreads
{
    public sealed class GameInvitationListenerThread : BaseGameThread, IGameThread
    {
        public GameInvitationListenerThread(CancellationPool cancellationPool) : base(cancellationPool)
        {
        }

        public override async Task RunAsync()
        {
            base.HostRun(internalRunAsync);
        }

        private Task internalRunAsync()
        {
            Console.WriteLine("Im GameInvitationListenerThread");
            return Task.CompletedTask;
        }
    }
}
