using Game.Jobs;
using Game.Processes.Implementations;
using Game.Processes.Orchestration;

namespace Game.Processes
{
    public abstract class BaseSlaveProcess : BaseProcess
    {
        protected BaseSlaveProcess(IEnumerable<IJob> jobs) : base(jobs)
        {
        }

        public override async Task StartAsync(string[] processJsonData)
        {
            await base.StartAsync(processJsonData);
            ProcessesOrchestrator.ReturnProcessControl<MainProcess>();
        }
    }
}
