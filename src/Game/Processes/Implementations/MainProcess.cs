using Game.Jobs;

namespace Game.Processes.Implementations
{
    public sealed class MainProcess : BaseGameProcess
    {
        public static string InternalId = "0";
        public MainProcess(IEnumerable<IJob> jobs) : base(jobs)
        {
        }

        public override async Task StartAsync()
        {
            await base.StartAsync();
            while(true)
            {
                Console.WriteLine("Main!");
                var x = Console.ReadLine();
                if (x=="1")
                {
                    ProcessesOrchestrator.StartProcess<InvitationCreatingProcess>();
                }
                if (x=="2")
                {
                    ProcessesOrchestrator.StartProcess<ChessGameProcess>();

                }
            }
   
        }
    }
}
