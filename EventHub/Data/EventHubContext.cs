using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            // Organizer - OrganizerProfile: One-to-One
            modelBuilder.Entity<OrganizerProfile>(entity =>
            {
                entity.HasKey(p => p.OrganizerProfileId);

                entity.Property(p => p.Biography)
                    .HasMaxLength(2000)
                    .IsRequired();

                entity.Property(p => p.WebsiteUrl)
                    .HasMaxLength(300);

                entity.Property(p => p.LogoUrl)
                    .HasMaxLength(300);

                entity.HasOne(p => p.Organizer)
                    .WithOne(o => o.Profile)
                    .HasForeignKey<OrganizerProfile>(
                        p => p.OrganizerId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Organizer - Event: One-to-Many
            modelBuilder.Entity<Event>(entity =>
            {

                entity.HasKey(e => e.EventId);

                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasMaxLength(4000)
                    .IsRequired();

                entity.Property(e => e.StartDate)
                    .IsRequired();

                entity.Property(e => e.EndDate)
                    .IsRequired(false);

                entity.Property(e => e.MaxAttendees)
                    .IsRequired();

                entity.HasOne(e => e.Organizer)
                    .WithMany(o => o.Events)
                    .HasForeignKey(e => e.OrganizerId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Badge>(entity =>
            {
                entity.HasKey(b => b.BadgeId);

                entity.Property(b => b.CredentialNumber)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.HasIndex(b => b.CredentialNumber)
                    .IsUnique();

                entity.Property(b => b.IssuedAt)
                    .IsRequired();

                entity.Property(b => b.Tier)
                    .HasConversion<string>()
                    .HasMaxLength(20)
                    .IsRequired();

                entity.HasOne(b => b.Attendee)
                    .WithOne(a => a.Badge)
                    .HasForeignKey<Badge>(b => b.AttendeeId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}