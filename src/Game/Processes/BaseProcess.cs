using Game.Jobs;

namespace Game.Processes
{
    public abstract class BaseProcess : IProcess
    {
        public abstract IEnumerable<Type> JobTypesToHost { get; }
        protected string[] _ProcessJsonData { get; set; } = Array.Empty<string>();
        private IEnumerable<IJob> _Jobs { get; }
        protected BaseProcess(IEnumerable<IJob> jobs)
        {
            _Jobs = jobs.Where(c=> JobTypesToHost.Contains(c.GetType()));
        }

        public virtual async Task StartAsync(string[] processJsonData)
        {
            _ProcessJsonData = processJsonData;
            var jobsStarts = _Jobs.Select(c => Task.Run(()=>c.RunAsync())).ToList();
            await ProcessMethodAsync();
        }

        public abstract Task ProcessMethodAsync();
    }
}
