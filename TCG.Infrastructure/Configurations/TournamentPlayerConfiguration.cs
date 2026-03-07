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

            builder.HasKey(tp => tp.TpId);

            builder.Property(tp => tp.TpId)
                .HasColumnName("tp_id")
                .ValueGeneratedOnAdd();

            builder.Property(tp => tp.TpTournament)
                .HasColumnName("tp_tournament");

            builder.Property(tp => tp.TpPlayerName)
                .HasColumnName("tp_player_name");

            builder.HasOne(tp => tp.Tournament)
                .WithMany(t => t.TournamentPlayers)
                .HasForeignKey(tp => tp.TpTournament);
        }
    }
}