using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TCG.Domain.Entities;

namespace TCG.Infrastructure.Configurations
{
    public class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder.ToTable("tournaments");

            builder.HasKey(t => t.TournamentId);

            builder.Property(t => t.TournamentId)
                   .HasColumnName("tournament_id");

            builder.Property(t => t.TournamentLeague)
                   .HasColumnName("tournament_league");

            builder.Property(t => t.TournamentName)
                   .HasColumnName("tournament_name");

            builder.Property(t => t.TournamentGame)
                   .HasColumnName("tournament_game");

            builder.Property(t => t.TournamentFormat)
                   .HasColumnName("tournament_format");

            builder.Property(t => t.TournamentRequireDeck)
                   .HasColumnName("tournament_require_deck");

            builder.Property(t => t.TournamentRoundNum)
                   .HasColumnName("tournament_round_num");

            builder.Property(t => t.TournamentDescription)
                   .HasColumnName("tournament_description");

            builder.Property(t => t.TournamentPairing)
                   .HasColumnName("tournament_pairing");

            builder.Property(t => t.TournamentDate)
                   .HasColumnName("tournament_calendar");

            builder.Property(t => t.TournamentEntryFee)
                   .HasColumnName("tournament_entry_fee")
                   .HasColumnType("numeric(4,2)");

            builder.Property(t => t.TournamentMaxParticipants)
                   .HasColumnName("tournament_max_participants");

            builder.HasOne<League>()
                   .WithMany()
                   .HasForeignKey(t => t.TournamentLeague);
        }
    }

}
