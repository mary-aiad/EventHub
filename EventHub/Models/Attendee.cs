using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Models
{
    internal class Attendee
    {
        [Key]
        public int AttendeeId { get; set; }

        [Required, MaxLength(150)]
        public string FullName { get; set; } = string.Empty;

        [Required, MaxLength(200), EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Street { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Country { get; set; } = string.Empty;

        [Required, MaxLength(20), Column("ZipCode")]
        public string PostalCode { get; set; } = string.Empty;

        public Badge? Badge { get; set; }
    }
}
