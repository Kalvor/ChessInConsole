using Game.Display;
using Game.Jobs;
using Game.Processes.Orchestration;
using Game.Tools;
using Networking.Models;
using Networking.Services.Interfaces;
using Newtonsoft.Json;

namespace Game.Processes.Implementations
{
    internal class InvitationHandlingProcess : BaseSlaveProcess
    {
        public static string InternalId = "4";
        private readonly MessagePrinter _MessagePrinter;
        private readonly OptionsPicker _OptionsPicker;
        private readonly InputReader _InputReader;
        private readonly INetworkAccessor _NetworkAccessor;
        private GameInvitationResponse _Response = new();
        private GameInvitation _Invitation;
        public InvitationHandlingProcess(IEnumerable<IJob> jobs, MessagePrinter messagePrinter, OptionsPicker optionsPicker, InputReader inputReader, INetworkAccessor networkAccessor) : base(jobs)
        {
            _MessagePrinter = messagePrinter;
            _OptionsPicker = optionsPicker;
            _InputReader = inputReader;
            _NetworkAccessor = networkAccessor;
        }

        public override IEnumerable<Type> JobTypesToHost => new[]
        {
            typeof(IJob)
        };

        public override async Task ProcessMethodAsync()
        {
            _Invitation = JsonConvert.DeserializeObject<GameInvitation>(_ProcessJsonData[0]);
            _Response.InvitationId = _Invitation.Id;
            _MessagePrinter.PrintText(DisplayTable.Header_Main);
            _MessagePrinter.PrintText(DisplayTable.Header_Sub_ResolveInvitation);

            _Response.Accepted = _OptionsPicker.PickOptions(
                DisplayTable.Input_Accept_Invitation,
                DisplayTable.Input_Decline_Invitation
             ) == 0;

            await _NetworkAccessor.SendDataAsync(_Invitation.InvitorHost!, JsonConvert.SerializeObject(_Response), default);
            if(_Response.Accepted)
            {
                ProcessesOrchestrator.RedirectProcessControl<InvitationHandlingProcess, OnlineChessGameProcess>(_Invitation);
            }
        }
    }
}
