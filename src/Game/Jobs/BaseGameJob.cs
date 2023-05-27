namespace Game.Jobs
{
    public abstract class BaseGameJob : IJob
    {
        public bool IsPaused => _CancellationPool.Pool[GetType()].IsCancellationRequested;

        protected readonly JobsCancellationPool _CancellationPool;
        protected BaseGameJob(JobsCancellationPool cancellationPool)
        {
            _CancellationPool = cancellationPool;
        }

        public virtual async Task RunAsync()
        {
            do
            {
                if (!IsPaused)
                {
                    await JobMethodAsync();
                }
            }
            while (true);
        }

        public virtual Task StopAsync()
        {
            _CancellationPool.PauseJob(this.GetType());
            return Task.CompletedTask;
        }

        public abstract Task JobMethodAsync();
    }
}
