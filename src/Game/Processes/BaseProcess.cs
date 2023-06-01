﻿using Game.Jobs;

namespace Game.Processes
{
    public abstract class BaseProcess : IProcess
    {
        public abstract IEnumerable<Type> JobTypesToHost { get; }
        private IEnumerable<IJob> _Jobs { get; }
        protected BaseProcess(IEnumerable<IJob> jobs)
        {
            _Jobs = jobs.Where(c=> JobTypesToHost.Contains(c.GetType()));
        }

        public virtual Task StartAsync()
        {
            var jobsStarts = _Jobs.Select(c => Task.Run(()=>c.RunAsync())).ToList();
            ProcessMethodAsync();
            return Task.CompletedTask;
        }

        public abstract Task ProcessMethodAsync();
    }
}
