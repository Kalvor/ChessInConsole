using Game.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Processes.Implementations
{
    public sealed class LocalChessGameProcess : BaseSlaveProcess
    {
        public static new string InternalId = "2";
        public LocalChessGameProcess(IEnumerable<IJob> jobs) : base(jobs)
        {
        }

        public override Task ProcessMethodAsync()
        {
            Console.WriteLine("SZACHY!");
            var x = Console.ReadLine();

            return Task.CompletedTask;
        }
    }
}
