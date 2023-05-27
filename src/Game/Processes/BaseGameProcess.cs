using Game.Jobs;

namespace Game.Processes
{
    public abstract class BaseGameProcess : IProcess
    {
        public IEnumerable<IJob> Jobs { get; }
        protected BaseGameProcess(IEnumerable<IJob> jobs)
        {
            Jobs = jobs;
        }

        public virtual async Task KillAsync()
        {
            var jobsCancallations =  Jobs.Select(c => c.StopAsync()).ToList();
            await Task.WhenAll(jobsCancallations);
        }

        public virtual Task StartAsync()
        {
            var jobsStarts = Jobs.Select(c => Task.Run(()=>c.RunAsync())).ToList();

            return Task.CompletedTask;
        }
    }
}
