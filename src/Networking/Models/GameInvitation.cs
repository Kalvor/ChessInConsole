using Networking.Data;

namespace Networking.Models
{
    public sealed class GameInvitation
    {
        public Guid Id { get; } = Guid.NewGuid();
        public int ClockBase { get; set; }
        public int ClockAdd { get; set; }
        public string InvitorName { get; set; }
        public Host InvitorHost { get; set; }
    }
}
