using Game.Jobs;

namespace Game.Processes.Implementations
{
    public class OnlineChessGameProcess : BaseSlaveProcess
    {
        public static string InternalId = "3";
        public OnlineChessGameProcess(IEnumerable<IJob> jobs) : base(jobs)
        {
        }

        public override IEnumerable<Type> JobTypesToHost => new[]
        {
            typeof(IJob)
        };

        public override Task ProcessMethodAsync()
        {
            Console.WriteLine("Online CHESS");
            return Task.CompletedTask;
        }
    }
}
