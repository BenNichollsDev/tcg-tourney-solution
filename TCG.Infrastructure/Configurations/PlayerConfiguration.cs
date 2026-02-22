using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TCG.Domain.Entities;

namespace TCG.Infrastructure.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("players");

            builder.HasKey(p => p.PlayerId);

            builder.Property(p => p.PlayerId)
                   .HasColumnName("player_id");

            builder.Property(p => p.PlayerFirstName)
                   .HasColumnName("player_first_name");

            builder.Property(p => p.PlayerSurname)
                   .HasColumnName("player_surname");

            builder.Property(p => p.PlayerDob)
                   .HasColumnName("player_dob");

            builder.Property(p => p.PlayerEmail)
                   .HasColumnName("player_email");

            builder.Property(p => p.PlayerMobile)
                   .HasColumnName("player_mobile");
        }
    }

}
