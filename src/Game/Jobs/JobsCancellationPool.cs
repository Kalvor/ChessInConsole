using System.Collections.Concurrent;

namespace Game.Jobs
{
    public sealed class JobsCancellationPool
    {
        public JobsCancellationPool(IEnumerable<Type> jobs)
        {
            _pool = new ConcurrentDictionary<Type, CancellationTokenSource>(jobs.ToDictionary(c => c, c => new CancellationTokenSource()));
        }

        private ConcurrentDictionary<Type, CancellationTokenSource> _pool;

        public ConcurrentDictionary<Type, CancellationTokenSource> Pool => _pool;

        public void PauseJob<TGameJob>() where TGameJob : IJob
        {
            PauseJob(typeof(TGameJob));
        }

        public void PauseJob(Type type)
        {
            _pool[type].Cancel();
        }

        public void ResumeJob<TGameJob>() where TGameJob : IJob
        {
            _pool[typeof(TGameJob)] = new CancellationTokenSource();
        }

        public void PauseAll()
        {
            _pool.Select(c=> { c.Value.Cancel(); return 0; }).ToArray();
        }
    }
}
