using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TCG.Domain.Entities;

namespace TCG.Infrastructure.Configurations
{
    public class TournamentPlayerConfiguration : IEntityTypeConfiguration<TournamentPlayer>
    {
        public void Configure(EntityTypeBuilder<TournamentPlayer> builder)
        {
            builder.ToTable("tournament_players");

            builder.HasKey(tp => tp.TpId);

            builder.Property(tp => tp.TpId)
                   .HasColumnName("tp_id");

            builder.Property(tp => tp.TpTournament)
                   .HasColumnName("tp_tournament");

            builder.Property(tp => tp.TpPlayer)
                   .HasColumnName("tp_player");

            builder.HasOne<Tournament>()
                   .WithMany()
                   .HasForeignKey(tp => tp.TpTournament);

            builder.HasOne<Player>()
                   .WithMany()
                   .HasForeignKey(tp => tp.TpPlayer);
        }
    }

}
