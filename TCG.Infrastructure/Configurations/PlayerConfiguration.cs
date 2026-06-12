using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCG.Domain.Entities;

namespace TCG.Infrastructure.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("players");

            builder.HasKey(p => p.PlayerId);

            builder.Property(p => p.PlayerId).HasColumnName("player_id");
            builder.Property(p => p.PlayerFirstName).HasColumnName("player_first_name");
            builder.Property(p => p.PlayerLastName).HasColumnName("player_last_name");
            builder.Property(p => p.PlayerEmail).HasColumnName("player_email");
            builder.Property(p => p.PlayerPhone).HasColumnName("player_phone");
            builder.Property(p => p.PlayerAge).HasColumnName("player_age");
            builder.Property(p => p.PlayerGender).HasColumnName("player_gender");
        }
    }
}
