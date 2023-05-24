namespace Game.Threads.GameThreads
{
    public abstract class BaseGameThread : IGameThread
    {
        public virtual bool IsCancaled { get; private set; } = false;
        public bool IsPaused => this._CancellationPool.Pool[this.GetType()].IsCancellationRequested;

        protected readonly CancellationPool _CancellationPool;
        protected BaseGameThread(CancellationPool cancellationPool)
        {
            _CancellationPool = cancellationPool;
        }

        public virtual Task RunAsync()
        {
            throw new NotImplementedException();
        }
        public virtual Task StopAsync()
        {
            this.IsCancaled = true;
            return Task.CompletedTask;  
        }
        protected void HostRun(Func<Task> run)
        {
            do
            {
                if (!this.IsPaused)
                {
                    run();
                }
            }
            while (!this.IsCancaled);
        }
        
    }
}
