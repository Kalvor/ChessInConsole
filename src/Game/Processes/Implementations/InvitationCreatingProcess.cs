using Game.Display;
using Game.Jobs;
using Game.Jobs.Implementations;
using Game.Processes.Orchestration;
using Game.Tools;
using Networking.Models;
using Networking.Services.Interfaces;
using Newtonsoft.Json;
using System.Net;

namespace Game.Processes.Implementations
{
    public sealed class InvitationCreatingProcess : BaseSlaveProcess
    {
        public static string InternalId = "1";
        private readonly MessagePrinter _MessagePrinter;
        private readonly OptionsPicker _OptionsPicker;
        private readonly InputReader _InputReader;
        private readonly INetworkAccessor _NetworkAccessor;
        private GameInvitation _Invitation = new();
        public InvitationCreatingProcess(IEnumerable<IJob> jobs, MessagePrinter messagePrinter, OptionsPicker optionsPicker, InputReader inputReader, INetworkAccessor networkAccessor) : base(jobs)
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
            _MessagePrinter.PrintText(DisplayTable.Header_Main);
            _MessagePrinter.PrintText(DisplayTable.Header_Sub_CreateInvitation);
            _MessagePrinter.PrintText(DisplayTable.Input_Name_CreateInvitation);
            _Invitation.InvitorName = _InputReader.ReadString();

            _MessagePrinter.PrintText(DisplayTable.Input_ClockBase_CreateInvitation);
            _Invitation.ClockBase = _InputReader.ReadInt();

            _MessagePrinter.PrintText(DisplayTable.Input_ClockAdd_CreateInvitation);
            _Invitation.ClockAdd = _InputReader.ReadInt();

            _MessagePrinter.PrintText(DisplayTable.Input_PiecesColor_Create_Invitation);
            _Invitation.PiecesColor = (PiecesColorEnum)_OptionsPicker.PickOptions(
                DisplayTable.Input_PiecesColorBlack_Create_Invitation,
                DisplayTable.Input_PiecesColorWhite_Create_Invitation,
                DisplayTable.Input_PiecesColorRandom_Create_Invitation
             );

            _MessagePrinter.PrintText(DisplayTable.Input_ReceiverIP_CreateInvitation);
            _Invitation.InvitedHost = new Networking.Data.Host(_InputReader.ReadString());
            _Invitation.InvitorHost = _NetworkAccessor.GetLocalHost();

            try
            {
                _MessagePrinter.PrintText(DisplayTable.Input_Listening_CreateInvitation);
                await _NetworkAccessor.SendDataAsync(_Invitation.InvitedHost, JsonConvert.SerializeObject(_Invitation));
            }
            catch(Exception e)
            {
                _MessagePrinter.PrintText(DisplayTable.Input_Error_CreateInvitation);
                Thread.Sleep(5000);
                return;
            }

            var data = await _NetworkAccessor.ListenFromDataAsync<GameInvitationResponse>(_Invitation.InvitedHost);
            if(data.Accepted)
            {
                ProcessesOrchestrator.RedirectProcessControl<InvitationCreatingProcess, OnlineChessGameProcess>(_Invitation);
            }
            else
            {
                _MessagePrinter.PrintText(DisplayTable.Input_Declined_CreateInvitation);
                Thread.Sleep(5000);
            }

        }
    }
}
