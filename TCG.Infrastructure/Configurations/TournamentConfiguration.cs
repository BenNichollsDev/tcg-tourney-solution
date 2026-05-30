using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
                .HasColumnName("tournament_id")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.LeagueId)
                .HasColumnName("tournament_league");

            builder.Property(t => t.TournamentName).IsRequired();
            builder.Property(t => t.TournamentGame).IsRequired();
            builder.Property(t => t.TournamentFormat).IsRequired();

            builder.Property(t => t.TournamentRequireDeck)
                .HasColumnName("tournament_require_deck");

            builder.Property(t => t.TournamentRoundNum)
                .HasColumnName("tournament_round_num");

            builder.Property(t => t.TournamentDescription)
                .HasColumnName("tournament_description");

            builder.Property(t => t.TournamentPairing)
                .HasColumnName("tournament_pairing");

            builder.Property(t => t.TournamentDate)
                .HasColumnName("tournament_date");

            builder.Property(t => t.TournamentTime)
                .HasColumnName("tournament_time");

            builder.Property(t => t.TournamentEntryFee)
                .HasColumnType("numeric(10,2)");

            builder.Property(t => t.TournamentMaxParticipants)
                .HasColumnName("tournament_max_participants");

            builder.HasMany(t => t.TournamentPlayers)
                .WithOne(tp => tp.Tournament)
                .HasForeignKey(tp => tp.TournamentId);

            builder.HasOne(t => t.League)
                .WithMany(l => l.Tournaments)
                .HasForeignKey(t => t.LeagueId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}