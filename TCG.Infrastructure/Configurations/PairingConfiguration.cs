using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCG.Domain.Entities;

namespace TCG.Infrastructure.Configurations
{
    public class PairingConfiguration : IEntityTypeConfiguration<Pairing>
    {
        public void Configure(EntityTypeBuilder<Pairing> builder)
        {
            builder.ToTable("pairings");

            builder.HasKey(p => p.PairingId);

            builder.Property(p => p.PairingId)
                .HasColumnName("pairing_id")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.PairingTp1)
                .HasColumnName("pairing_tp_1");

            builder.Property(p => p.PairingTp2)
                .HasColumnName("pairing_tp_2");

            builder.Property(p => p.PairingWinner)
                .HasColumnName("pairing_winner");

            builder.HasOne<TournamentPlayer>()
                .WithMany()
                .HasForeignKey(p => p.PairingTp1)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<TournamentPlayer>()
                .WithMany()
                .HasForeignKey(p => p.PairingTp2)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}