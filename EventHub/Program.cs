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
            #region Add Data
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
            #endregion

            #region SelectOrganizerData
            var organizers = db.Organizers
                .AsNoTracking()
                .Select(o => new
                {
                    o.Name,
                    o.CompanyName,
                    o.Profile.WebsiteUrl,

                    Events = o.Events.Select(e => new
                    {
                        e.Title,
                        e.StartDate,
                        e.MaxAttendees
                    }).ToList()
                })
                .ToList();

            foreach (var organ in organizers)
            {
                Console.WriteLine($"Organizer: {organ.Name}");
                Console.WriteLine($"Company: {organ.CompanyName ?? "N/A"}");
                Console.WriteLine($"Website: {organ.WebsiteUrl ?? "N/A"}");

                foreach (var ev in organ.Events)
                {
                    Console.WriteLine($"Event: {ev.Title}");
                    Console.WriteLine($"Start: {ev.StartDate}");
                    Console.WriteLine($"Capacity: {ev.MaxAttendees}");
                }
            }

            #endregion

            #region SelectAttendeesData
            var attendees = db.Attendees
                .AsNoTracking()
                .Select(o => new
                {
                    o.FullName,
                    o.Email,
                    o.City,
                    o.Country,
                    o.PostalCode,
                    BadgeNumber = o.Badge == null
                    ? null
                    : o.Badge.CredentialNumber,
                    BadgeTier = o.Badge == null
                    ? null
                    : (BadgeTier?)o.Badge.Tier
                }).ToList();


            foreach (var attend in attendees)
            {
                Console.WriteLine($"Name: {attend.FullName}");
                Console.WriteLine($"Email: {attend.Email}");
                Console.WriteLine($"City: {attend.City}");
                Console.WriteLine($"Country: {attend.Country}");
                Console.WriteLine($"Postal Code: {attend.PostalCode}");

                Console.WriteLine(
                    $"Badge: {attend.BadgeNumber ?? "No Badge"}");

                Console.WriteLine(
                    $"Tier: {attend.BadgeTier?.ToString() ?? "N/A"}");
            }
            #endregion
        }
    }
}
