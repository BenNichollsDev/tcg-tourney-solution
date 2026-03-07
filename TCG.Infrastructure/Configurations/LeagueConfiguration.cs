using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCG.Domain.Entities;

namespace TCG.Infrastructure.Configurations
{
    public class LeagueConfiguration : IEntityTypeConfiguration<League>
    {
        public void Configure(EntityTypeBuilder<League> builder)
        {
            builder.ToTable("leagues");

            builder.HasKey(l => l.LeagueId);

            builder.Property(l => l.LeagueId)
                .HasColumnName("league_id")
                .ValueGeneratedOnAdd();

            builder.Property(l => l.LeagueName)
                .HasColumnName("league_name");

            builder.Property(l => l.LeagueGame)
                .HasColumnName("league_game");

            builder.Property(l => l.LeagueDescription)
                .HasColumnName("league_description");
        }
    }
}