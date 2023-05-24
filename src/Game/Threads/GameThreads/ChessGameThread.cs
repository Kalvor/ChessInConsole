namespace Game.Threads.GameThreads
{
    public sealed class ChessGameThread : BaseGameThread, IGameThread
    {
        public ChessGameThread(CancellationPool cancellationPool) : base(cancellationPool)
        {
        }

        public override async Task RunAsync()
        {
            base.HostRun(internalRunAsync);
        }

        private Task internalRunAsync()
        {
            Console.WriteLine("Im ChessGameThread");
            return Task.CompletedTask;
        }
    }
}
