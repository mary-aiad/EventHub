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
    public class BadgeConfig : IEntityTypeConfiguration<Badge>
    {
        void IEntityTypeConfiguration<Badge>.Configure(EntityTypeBuilder<Badge> builder)
        {
            builder.HasKey(b => b.BadgeId);

            builder.Property(b => b.CredentialNumber)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(b => b.CredentialNumber)
                .IsUnique();

            builder.Property(b => b.IssuedAt)
                .IsRequired();

            builder.Property(b => b.Tier)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.HasOne(b => b.Attendee)
                .WithOne(a => a.Badge)
                .HasForeignKey<Badge>(b => b.AttendeeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
