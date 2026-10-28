using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Models
{
    [Table("EventOrganizers")]
    internal class Organizer
    {
        [Key]
        public int OrganizerId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(150)]
        public string? CompanyName { get; set; }
        public OrganizerProfile? Profile { get; set; }

        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
