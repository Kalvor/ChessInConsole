using Game.Tools;

namespace Game.Threads.GameThreads
{
    public sealed class ProgramHostThread : BaseGameThread, IGameThread
    {
        private readonly InputReader _InputReader;
        public ProgramHostThread(CancellationPool cancellationPool, InputReader inputReader) : base(cancellationPool)
        {
            _InputReader = inputReader;
        }

        public override Task RunAsync()
        {
            base.HostRun(() =>
            {
                Task listenerTask = _InputReader.ListenForKey(ConsoleKey.Escape);
                if (listenerTask.IsCompleted)
                {
                    _CancellationPool.PauseAll();
                }
                return Task.CompletedTask;
            });
            return Task.CompletedTask;
        }
    }
}
