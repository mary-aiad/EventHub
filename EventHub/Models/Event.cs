using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Models
{
    internal class Event
    {
        public int EventId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int MaxAttendees { get; set; }

        public int OrganizerId { get; set; }

        public Organizer Organizer { get; set; } = null!;
    }
}
