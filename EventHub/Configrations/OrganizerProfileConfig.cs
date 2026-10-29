using EventHub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Configrations
{
    public class OrganizerProfileConfig : IEntityTypeConfiguration<OrganizerProfile>
    {
       
        void IEntityTypeConfiguration<OrganizerProfile>.Configure(EntityTypeBuilder<OrganizerProfile> typeBuilder)
        {
            typeBuilder.HasKey(p => p.OrganizerProfileId);

            typeBuilder.Property(p => p.Biography)
                .HasMaxLength(2000)
                .IsRequired();

            typeBuilder.Property(p => p.WebsiteUrl)
                .HasMaxLength(300);

            typeBuilder.Property(p => p.LogoUrl)
                .HasMaxLength(300);

            typeBuilder.HasOne(p => p.Organizer)
                .WithOne(o => o.Profile)
                .HasForeignKey<OrganizerProfile>(
                    p => p.OrganizerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
