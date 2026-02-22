using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TCG.Domain.Entities;

namespace TCG.Infrastructure.Configurations
{
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.ToTable("matches");

            builder.HasKey(m => m.MatchId);

            builder.Property(m => m.MatchId)
                   .HasColumnName("match_id");

            builder.Property(m => m.PairingId)
                   .HasColumnName("pairing_id");

            builder.Property(m => m.MatchRoundNum)
                   .HasColumnName("match_round_num");

            builder.Property(m => m.Player1Winner)
                   .HasColumnName("player_1_winner");

            builder.Property(m => m.Player2Winner)
                   .HasColumnName("player_2_winner");

            builder.HasOne<Pairing>()
                   .WithMany()
                   .HasForeignKey(m => m.PairingId);
        }
    }

}
