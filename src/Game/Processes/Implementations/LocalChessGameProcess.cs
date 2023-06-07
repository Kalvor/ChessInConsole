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
        private readonly MessagePrinter _MessagePrinter;
        public LocalChessGameProcess(IEnumerable<IJob> jobs, BoardPrinter boardPrinter) : base(jobs)
        {
            _BoardPrinter = boardPrinter;
        }

        public override IEnumerable<Type> JobTypesToHost => new[] { typeof(IJob) };

        public override Task ProcessMethodAsync()
        {
            ChessGame currentGame = ChessGame.Begin(Color.White);
            while (currentGame.IsPending)
            {
                _BoardPrinter.Print(currentGame);
                try
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    if(currentGame.CurrentPlayerColor!= Color.White)
                    {
                        Console.WriteLine("Black to move");
                    }
                    else
                    {
                        Console.WriteLine("White to move");
                    }
                    Console.WriteLine("Enter your move in like e2 e4");
                    Console.WriteLine();
                    Console.Write("Move : ");
                    var move = currentGame.ReadMove();
                    bool isValid = currentGame.CanDoMove(move);
                    if (isValid)
                    {
                        currentGame.DoMove(move);
                    }
                    else
                    {
                        Console.WriteLine("Invalid move");
                        Thread.Sleep(2000);
                    }
                    Console.Clear();
                }
                catch(Exception e) {
                    throw;
                }
            }

            return Task.CompletedTask;
        }
    }
}
