using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Models
{
    public enum BadgeTier
    {
        Standard,
        VIP
    }
    internal class Badge
    {
        public int BadgeId { get; set; }

        public string CredentialNumber { get; set; } = string.Empty;

        public DateTime IssuedAt { get; set; }

        public BadgeTier Tier { get; set; }

        public int AttendeeId { get; set; }

        public Attendee Attendee { get; set; } = null!;

    }
}
