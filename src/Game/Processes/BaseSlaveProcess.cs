using Game.Jobs;
using Game.Processes.Implementations;

namespace Game.Processes
{
    public abstract class BaseSlaveProcess : BaseProcess
    {
        protected BaseSlaveProcess(IEnumerable<IJob> jobs) : base(jobs)
        {
        }

        public override async Task StartAsync()
        {
            await base.StartAsync();
            ReturnControllToMain();
        }

        public void ReturnControllToMain()
        {
            ProcessesOrchestrator.ResumeProcess<MainProcess>();
            ProcessesOrchestrator.KillProcess(this.GetType());
        }
    }
}
