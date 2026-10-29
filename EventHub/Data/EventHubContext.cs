using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventHub.Configrations;
using EventHub.Models;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Data
{
    internal class EventHubContext : DbContext
    {
        public DbSet<Organizer> Organizers => Set<Organizer>();
        public DbSet<OrganizerProfile> OrganizerProfiles => Set<OrganizerProfile>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Attendee> Attendees => Set<Attendee>();
        public DbSet<Badge> Badges => Set<Badge>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=EventHubDb;Trusted_Connection=True;TrustServerCertificate=True");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new OrganizerProfileConfig());
            modelBuilder.ApplyConfiguration(new BadgeConfig());
            modelBuilder.ApplyConfiguration(new EventConfig());
        }
    }
}