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
                Console.WriteLine(ProcessesOrchestrator._OpenProcesses.Count);
                var x = Console.ReadLine();
                if (x=="1")
                {
                    ProcessesOrchestrator.StartProcess<InvitationCreatingProcess>();
                    ProcessesOrchestrator.SuspendProcess<MainProcess>();
                    ProcessesOrchestrator.KillProcess<InvitationCreatingProcess>();
                }
                if (x=="2")
                {
                    ProcessesOrchestrator.StartProcess<ChessGameProcess>();
                    ProcessesOrchestrator.SuspendProcess<MainProcess>();
                    ProcessesOrchestrator.KillProcess<ChessGameProcess>();

                }
                Console.WriteLine("Im resumed");
            }
   
        }
    }
}
