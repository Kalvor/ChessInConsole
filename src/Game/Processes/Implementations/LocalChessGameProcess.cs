using Chess;
using Chess.Data;
using Game.Jobs;
using Game.Tools;

namespace Game.Processes.Implementations
{
    public sealed class LocalChessGameProcess : BaseProcess
    {
        public static string InternalId = "2";
        private readonly BoardPrinter _BoardPrinter;
        public LocalChessGameProcess(IEnumerable<IJob> jobs, BoardPrinter boardPrinter) : base(jobs)
        {
            _BoardPrinter = boardPrinter;
        }

        public override IEnumerable<Type> JobTypesToHost => new[] { typeof(IJob) };

        public override Task ProcessMethodAsync()
        {
            ChessGame currentGame = ChessGame.Begin();
            _BoardPrinter.Print(currentGame);
            var x = Console.ReadLine();

            return Task.CompletedTask;
        }
    }
}
