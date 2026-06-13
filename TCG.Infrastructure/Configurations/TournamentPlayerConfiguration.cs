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
                .HasColumnName("tp_player_name")
                .HasColumnType("text");

            // Round-robin
            builder.Property(tp => tp.PlayerRoundRobinWins)
                .HasColumnName("player_rr_wins");

            builder.Property(tp => tp.PlayerRoundRobinDraws)
                .HasColumnName("player_rr_draws");

            builder.Property(tp => tp.PlayerRoundRobinLosses)
                .HasColumnName("player_rr_losses");

            builder.Property(tp => tp.PlayerRoundRobinScore)
                .HasColumnName("player_rr_score");

            builder.Property(tp => tp.PlayerRoundRobinMatchPoints)
                .HasColumnName("player_rr_match_points");

            builder.Property(tp => tp.PlayerRoundRobinPoints)
                .HasColumnName("player_rr_points");

            // Swiss
            builder.Property(tp => tp.PlayerSwissWins)
                .HasColumnName("player_sw_wins");

            builder.Property(tp => tp.PlayerSwissDraws)
                .HasColumnName("player_sw_draws");

            builder.Property(tp => tp.PlayerSwissLosses)
                .HasColumnName("player_sw_losses");

            builder.Property(tp => tp.PlayerSwissScore)
                .HasColumnName("player_sw_score");

            builder.Property(tp => tp.PlayerSwissMatchPoints)
                .HasColumnName("player_sw_match_points");

            builder.Property(tp => tp.PlayerSwissPoints)
                .HasColumnName("player_sw_points");

            builder.Property(tp => tp.PlayerBye)
                .HasColumnName("tp_byes");

            builder.Property(tp => tp.GamesPlayed)
                .HasColumnName("games_played");
        }
    }
}