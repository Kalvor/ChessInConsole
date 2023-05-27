using Game.Display;
using Game.Jobs;
using Game.Jobs.Implementations;
using Game.Tools;

namespace Game.Processes.Implementations
{
    public sealed class MainProcess : BaseProcess
    {
        public static string InternalId = "0";
        private readonly MessagePrinter _MessagePrinter;
        private readonly OptionsPicker _OptionsPicker;
        private readonly JobsCancellationPool _JobsCancellation;
        public MainProcess(IEnumerable<IJob> jobs, MessagePrinter messagePrinter, OptionsPicker optionsPicker, JobsCancellationPool jobsCancellation) : base(jobs)
        {
            _MessagePrinter = messagePrinter;
            _OptionsPicker = optionsPicker;
            _JobsCancellation = jobsCancellation;
        }

        public override Task ProcessMethodAsync()
        {
            while (true)
            {
                _MessagePrinter.PrintText(DisplayTable.HeaderText_1);
                int pickedOption = _OptionsPicker.PickOptions(
                    DisplayTable.MainOption_1,
                    DisplayTable.MainOption_2
                );

                _JobsCancellation.PauseJob<InvitationListenerJob>();
                Action processRedirectionAction = pickedOption switch
                {
                    0 => RedirectProcessControl<LocalChessGameProcess>,
                    1 => RedirectProcessControl<InvitationCreatingProcess>,
                    _ => throw new NotImplementedException()
                };
                processRedirectionAction();

                _JobsCancellation.ResumeJob<InvitationListenerJob>();
                _MessagePrinter.ClearText(DisplayTable.MainOption_1);
                _MessagePrinter.ClearText(DisplayTable.MainOption_2);
            }
        }

        private void RedirectProcessControl<TProcess>() where TProcess : IProcess
        {
            ProcessesOrchestrator.StartProcess<TProcess>();
            ProcessesOrchestrator.SuspendProcess<MainProcess>();
            ProcessesOrchestrator.KillProcess(typeof(TProcess));
        }
    }
}
