using Game.Processes;
using Game.Tools;

namespace Game.Jobs.Implementations
{
    public sealed class ProgramExitListenerJob : BaseGameJob
    {
        private readonly InputReader _InputReader;
        public ProgramExitListenerJob(CancellationPool cancellationPool, InputReader inputReader) : base(cancellationPool)
        {
            _InputReader = inputReader;
        }

        public override async Task RunAsync()
        {
            await base.HostRun(() =>
            {
                //Task listenerTask = _InputReader.ListenForKey(ConsoleKey.Escape);
                //if (listenerTask.IsCompleted)
                //{
                //    _CancellationPool.PauseAll();
                //    ProcessesOrchestrator.KillAllProcesses();
                //}
                return Task.CompletedTask;
            });
        }
    }
}
