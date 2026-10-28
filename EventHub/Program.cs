using EventHub.Data;
using EventHub.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EventHub
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var db = new EventHubContext();

            var organizer = new Organizer
            {
                Name = "Ahmed Ali",
                CompanyName = "Ali Events"
            };

            organizer.Profile = new OrganizerProfile
            {
                Biography = "Ali event organizer",
                WebsiteUrl = "https://example.com",
                LogoUrl = null
            };

            organizer.Events.Add(new Event
            {
                Title = ".Net Workshop",
                Description = "A workshop about Entity Framework Core.",
                StartDate = new DateTime(2026, 11, 15, 10, 0, 0),
                EndDate = new DateTime(2026, 11, 15, 16, 0, 0),
                MaxAttendees = 100
            });

            db.Organizers.Add(organizer);
            

            var attendee = new Attendee
            {
                FullName = "Mona Hassan",
                Email = "mona@example.com",
                Street = "Tahrir Street",
                City = "Cairo",
                Country = "Egypt",
                PostalCode = "11511",
                Badge = new Badge
                {
                    CredentialNumber = "1001",
                    IssuedAt = DateTime.UtcNow,
                    Tier = BadgeTier.VIP
                }
            };

            db.Attendees.Add(attendee);

            db.SaveChanges();

            Console.WriteLine("Sample data saved successfully.");

            var attendees = db.Attendees.Include(a => a.Badge).ToList();
            var organizers = db.Organizers.Include(o => o.Profile).Include(o => o.Events).ToList();

            foreach (var _organizer in organizers)
            {
                Console.WriteLine($"Organizer: {_organizer.Name}");
                Console.WriteLine( $"Company: {_organizer.CompanyName}");
                Console.WriteLine($"Website: {_organizer.Profile?.WebsiteUrl}");

                foreach (var ev in organizer.Events)
                {
                    Console.WriteLine($"Event: {ev.Title}");
                    Console.WriteLine($"Start: {ev.StartDate}");
                    Console.WriteLine($"Capacity: {ev.MaxAttendees}");
                }
            }

            foreach (var _attendee in attendees)
            {
                Console.WriteLine($"Attendee: {_attendee.FullName}");
                Console.WriteLine($"Email: {_attendee.Email}");

                if (_attendee.Badge != null)
                {
                    Console.WriteLine($"Badge: {_attendee.Badge.CredentialNumber}");

                    Console.WriteLine($"Tier: {_attendee.Badge.Tier}");
                }
            }
        }
    }
}
