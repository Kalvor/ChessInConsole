﻿using Networking.Data;

namespace Networking.Models
{
    public sealed class GameInvitation
    {
        public int ClockBase { get; set; }
        public int ClockAdd { get; set; }
        public string InvitorName { get; set; }
        public Host InvitorHost { get; set; }
    }
}