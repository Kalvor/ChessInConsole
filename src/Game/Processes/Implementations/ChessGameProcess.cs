using Game.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Processes.Implementations
{
    public sealed class ChessGameProcess : BaseGameProcess
    {
        public static new string InternalId = "2";
        public ChessGameProcess(IEnumerable<IJob> jobs) : base(jobs)
        {
        }

        public override async Task StartAsync()
        {
            await base.StartAsync();

            Console.WriteLine("SZACHY!");
            var x = Console.ReadLine();
            
        }
    }
}
