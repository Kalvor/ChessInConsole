using Game.Data.Models;
using Game.Threads;

namespace Game
{
    public sealed class ConsoleGameHost
    {
        private readonly IEnumerable<IGameThread> _GameThreads;
        public ConsoleGameHost(IEnumerable<IGameThread> gameThreads)
        {
            _GameThreads = gameThreads;
        }

        public Task RegisterInNetworkAsync(PlayerMetadata playerMetadata)
        {
            // throw new NotImplementedException();
            return Task.CompletedTask;
        }

        public Task RunGameThreadsAsync()
        {
            var gameProcesses = _GameThreads
                .Select(c => Task.Factory.StartNew(()=>c.RunAsync()))
                .ToArray();
            while (!_GameThreads.All(c => c.IsPaused))
            {

            }
            return Task.CompletedTask;
        }

        public async Task StopGameThreadsAsync() 
        {
            await Task.WhenAll(
                _GameThreads
                   .Select(c => Task.Factory.StartNew(() => c.StopAsync()))
                   .ToArray()
            );
        }
    }
}
