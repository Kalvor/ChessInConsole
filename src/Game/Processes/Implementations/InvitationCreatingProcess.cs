using Game.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Processes.Implementations
{
    public sealed class InvitationCreatingProcess : BaseGameProcess
    {
        public static new string InternalId = "1";
        public InvitationCreatingProcess(IEnumerable<IJob> jobs) : base(jobs)
        {
        }

        public override async Task StartAsync()
        {
            await base.StartAsync();

            Console.WriteLine("INV!");
            var x = Console.ReadLine();

        }
    }
}
