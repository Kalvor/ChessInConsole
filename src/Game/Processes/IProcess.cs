using Game.Jobs;

namespace Game.Processes
{
    public interface IProcess
    {
        public Task StartAsync();
        public Task KillAsync();
        public IEnumerable<IJob> Jobs { get; }
    }
}
