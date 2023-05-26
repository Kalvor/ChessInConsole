using Game.Data.Enums;

namespace Game.Tools
{
    public sealed class InputReader
    {
        public GameInvitationChoice GetGameInvitationChoice() { return default; }
        //public PlayerMetadata GetPlayerMetadata() { return null; }
        public Task ListenForKey(ConsoleKey key)
        {
            while(Console.ReadKey().Key != key)
            {

            }
            return Task.CompletedTask;
        }
    }
}
