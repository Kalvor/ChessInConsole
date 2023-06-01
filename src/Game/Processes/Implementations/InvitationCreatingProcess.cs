using Game.Display;
using Game.Jobs;
using Game.Jobs.Implementations;
using Game.Tools;
using Networking.Models;

namespace Game.Processes.Implementations
{
    public sealed class InvitationCreatingProcess : BaseSlaveProcess
    {
        public static string InternalId = "1";
        private readonly MessagePrinter _MessagePrinter;
        private readonly OptionsPicker _OptionsPicker;
        private readonly JobsCancellationPool _JobsCancellation;
        private GameInvitation _Invitation;
        public InvitationCreatingProcess(IEnumerable<IJob> jobs, MessagePrinter messagePrinter, OptionsPicker optionsPicker, JobsCancellationPool jobsCancellation) : base(jobs)
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
            _Invitation = new GameInvitation();
            _MessagePrinter.PrintText(DisplayTable.Header_Main);
            _MessagePrinter.PrintText(DisplayTable.Header_Sub_CreateInvitation);
            _MessagePrinter.PrintText(DisplayTable.Input_Name_CreateInvitation);
            _Invitation.InvitorName = Console.ReadLine();

            _MessagePrinter.PrintText(DisplayTable.Input_ClockBase_CreateInvitation);
            _Invitation.ClockBase = int.Parse(Console.ReadLine());

            _MessagePrinter.PrintText(DisplayTable.Input_ClockAdd_CreateInvitation);
            _Invitation.ClockAdd = int.Parse(Console.ReadLine());

            _MessagePrinter.PrintText(DisplayTable.Input_PiecesColor_Create_Invitation);
            _Invitation.PiecesColor = _OptionsPicker.PickOptions(
                DisplayTable.Input_PiecesColorBlack_Create_Invitation,
                DisplayTable.Input_PiecesColorWhite_Create_Invitation,
                DisplayTable.Input_PiecesColorRandom_Create_Invitation
             );

            _MessagePrinter.PrintText(DisplayTable.Input_ReceiverIP_CreateInvitation);
            _Invitation.InvitorHost = new Networking.Data.Host(Console.ReadLine());

            return Task.CompletedTask;
        }
    }
}
