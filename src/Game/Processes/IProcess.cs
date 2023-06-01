using Game.Jobs;

namespace Game.Processes
{
    public interface IProcess
    {
        public Task StartAsync(string[] processJsonData);
        public Task ProcessMethodAsync();
        public IEnumerable<Type> JobTypesToHost { get; }

    }
}
