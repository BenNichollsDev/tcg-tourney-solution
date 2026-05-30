using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCG.Domain.Entities;

namespace TCG.Infrastructure.Configurations
{
    public class TournamentPlayerConfiguration : IEntityTypeConfiguration<TournamentPlayer>
    {
        public void Configure(EntityTypeBuilder<TournamentPlayer> builder)
        {
            builder.ToTable("tournament_players");

            builder.HasKey(tp => tp.TournamentPlayerId);

            builder.Property(tp => tp.TournamentPlayerId)
                .HasColumnName("tp_id")
                .ValueGeneratedOnAdd();

            builder.Property(tp => tp.TournamentId)
                .HasColumnName("tp_tournament");

            builder.Property(tp => tp.PlayerName)
                .HasColumnName("tp_player_name");

            builder.HasOne(tp => tp.Tournament)
                .WithMany(t => t.TournamentPlayers)
                .HasForeignKey(tp => tp.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}