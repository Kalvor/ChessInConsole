using Game.Jobs;

namespace Game.Processes
{
    public abstract class BaseProcess : IProcess
    {
        public IEnumerable<IJob> Jobs { get; }
        protected BaseProcess(IEnumerable<IJob> jobs)
        {
            Jobs = jobs;
        }

        public virtual Task StartAsync()
        {
            var jobsStarts = Jobs.Select(c => Task.Run(()=>c.RunAsync())).ToList();
            ProcessMethodAsync();
            return Task.CompletedTask;
        }

        public abstract Task ProcessMethodAsync();
    }
}
