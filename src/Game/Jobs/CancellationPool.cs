using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Text;

namespace Game.Jobs
{
    public sealed class CancellationPool
    {
        public CancellationPool(IEnumerable<Type> jobs)
        {
            _pool = new ConcurrentDictionary<Type, CancellationTokenSource>(jobs.ToDictionary(c => c, c => new CancellationTokenSource()));
        }

        private ConcurrentDictionary<Type, CancellationTokenSource> _pool;
        public ConcurrentDictionary<Type, CancellationTokenSource> Pool => _pool;

        public void PauseJob<TGameJob>() where TGameJob : IJob
        {
            _pool[typeof(TGameJob)].Cancel();
        }

        public void PauseJob(Type type)
        {
            _pool[type].Cancel();
        }


        public void ResumeJob<TGameJob>() where TGameJob : IJob
        {
            _pool[typeof(TGameJob)].TryReset();
        }

        public void PauseAll()
        {
            _pool.Select(c=> { c.Value.Cancel(); return 0; }).ToArray();
        }
    }
}
