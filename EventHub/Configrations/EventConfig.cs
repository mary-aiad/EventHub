using EventHub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Configrations
{
    public class EventConfig : IEntityTypeConfiguration<Event>
    {
        void IEntityTypeConfiguration<Event>.Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.EventId);

            builder.Property(e => e.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasMaxLength(4000)
                .IsRequired();

            builder.Property(e => e.StartDate)
                .IsRequired();

            builder.Property(e => e.EndDate)
                .IsRequired(false);

            builder.Property(e => e.MaxAttendees)
                .IsRequired();

            builder.HasOne(e => e.Organizer)
                .WithMany(o => o.Events)
                .HasForeignKey(e => e.OrganizerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
