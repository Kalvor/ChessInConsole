using Game.Tools;

namespace Game.Jobs
{
    public abstract class BaseGameJob : IJob
    {
        public bool IsPaused => _CancellationPool.Pool[GetType()].IsCancellationRequested;

        protected readonly CancellationPool _CancellationPool;
        protected BaseGameJob(CancellationPool cancellationPool)
        {
            _CancellationPool = cancellationPool;
        }

        public virtual Task RunAsync()
        {
            throw new NotImplementedException();
        }
        public virtual Task StopAsync()
        {
            _CancellationPool.PauseJob(this.GetType());
            return Task.CompletedTask;
        }
        protected async Task HostRun(Func<Task> run)
        {
            do
            {
                if (!IsPaused)
                {
                    await run();
                }
            }
            while (true);
        }
    }
}
