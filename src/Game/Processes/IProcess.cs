using Game.Jobs;

namespace Game.Processes
{
    public interface IProcess
    {
        public Task StartAsync();
        public Task ProcessMethodAsync();
        public IEnumerable<IJob> Jobs { get; }
    }
}
