using System.Collections.Concurrent;

namespace Game.Threads
{
    public sealed class CancellationPool
    {
        public CancellationPool(IEnumerable<Type> threads)
        {
            _pool = new ConcurrentDictionary<Type, CancellationTokenSource>(threads.ToDictionary(c => c, c => new CancellationTokenSource()));
        }

        private ConcurrentDictionary<Type, CancellationTokenSource> _pool;
        public ConcurrentDictionary<Type, CancellationTokenSource> Pool => _pool;

        public void PauseThread<TGameThread>() where TGameThread : IGameThread
        {
            _pool[typeof(TGameThread)].Cancel();
        }

        public void ResumeThread<TGameThread>() where TGameThread : IGameThread
        {
            _pool[typeof(TGameThread)].TryReset();
        }

        public void PauseAll()
        {
            _pool.Select(c=> { c.Value.Cancel(); return 0; }).ToArray();
        }

        public void ResumeAllExcept<TGameThread>() where TGameThread : IGameThread
        {
            _pool.Where(c => !c.Key.Equals(typeof(TGameThread))).Select(c => { c.Value.TryReset(); return 0; }).ToArray();
        }
    }
}
