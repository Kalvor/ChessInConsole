using Networking.Data;
using Networking.Models;

namespace Game.Tools
{
    public class MessageDisplayer
    {
        //public void DisplayPlayerInList(Player player) { }
        //public void DisplayGameInvitation(GameConfiguration gameConfiguration) { }
        public void AskForPlayerName() => Console.Write("Enter your name : " ); 
        public void AskForClockBase() => Console.Write("Enter time base : "); 
        public void AskForClockAdd() { }
        public void AskForInvitationReciever() { }
        public void DisplayInvitation(GameInvitation invitation) { }
        public void AskForInvitationSendConfirmation() { }
    }
}
