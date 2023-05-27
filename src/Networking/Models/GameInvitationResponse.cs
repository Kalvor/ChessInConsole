namespace Networking.Models
{
    public sealed class GameInvitationResponse
    {
        public Guid InvitationId { get; set; }
        public bool Accepted { get; set; }
    }
}
