using Game.Display;
using Game.Jobs;
using Game.Jobs.Implementations;
using Game.Processes.Orchestration;
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

        public override IEnumerable<Type> JobTypesToHost => new[] 
        { 
            typeof(InvitationRequestListenerJob)
        };

        public override Task ProcessMethodAsync()
        {
            while (true)
            {
                _MessagePrinter.PrintText(DisplayTable.Header_Main);
                int pickedOption = _OptionsPicker.PickOptions(
                    DisplayTable.MenuOption_Main_1,
                    DisplayTable.MenuOption_Main_2
                );

                _JobsCancellation.PauseJob<InvitationRequestListenerJob>();
                Action<object[]> processRedirectionAction = pickedOption switch
                {
                    0 => ProcessesOrchestrator.RedirectProcessControl<MainProcess, LocalChessGameProcess>,
                    1 => ProcessesOrchestrator.RedirectProcessControl<MainProcess, InvitationCreatingProcess>,
                    _ => throw new NotImplementedException()
                };
                processRedirectionAction(Array.Empty<object>());

                _JobsCancellation.ResumeJob<InvitationRequestListenerJob>();
                _MessagePrinter.ClearText(DisplayTable.MenuOption_Main_1);
                _MessagePrinter.ClearText(DisplayTable.MenuOption_Main_2);
            }
        }
    }
}
