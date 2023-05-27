using Game.Jobs;

namespace Game.Processes.Implementations
{
    public sealed class InvitationCreatingProcess : BaseSlaveProcess
    {
        public static new string InternalId = "1";
        public InvitationCreatingProcess(IEnumerable<IJob> jobs) : base(jobs)
        {

        }

        public override Task ProcessMethodAsync()
        {
            Console.WriteLine("INV!");
            var x = Console.ReadLine();

            return Task.CompletedTask;
        }
    }
}
