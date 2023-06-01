using Game.Jobs;

namespace Game.Processes.Implementations
{
    internal class InvitationHandlingProcess : BaseSlaveProcess
    {
        public static string InternalId = "4";
        public InvitationHandlingProcess(IEnumerable<IJob> jobs) : base(jobs)
        {
        }

        public override IEnumerable<Type> JobTypesToHost => new[]
        {
            typeof(IJob)
        };

        public override Task ProcessMethodAsync()
        {
            Console.WriteLine(_ProcessJsonData[0]);
            Console.ReadLine();
            return Task.CompletedTask;
            // throw new NotImplementedException();
        }
    }
}
