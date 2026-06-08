using Microsoft.EntityFrameworkCore;
using TCG.Domain.Entities;

namespace TCG.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<League> Leagues { get; set; }

        public virtual DbSet<Tournament> Tournaments { get; set; }

        public virtual DbSet<Pairing> Pairings { get; set; }

        public virtual DbSet<Staff> Staff { get; set; }

        public virtual DbSet<TournamentPlayer> TournamentPlayers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<Pairing>(entity =>
            {
                entity.HasOne(p => p.Tournament)
                    .WithMany(t => t.Pairings)
                    .HasForeignKey(p => p.TournamentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.Player1)
                    .WithMany(tp => tp.PairingsAsPlayer1)
                    .HasForeignKey(p => p.Player1Id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Player2)
                    .WithMany(tp => tp.PairingsAsPlayer2)
                    .HasForeignKey(p => p.Player2Id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Winner)
                    .WithMany()
                    .HasForeignKey(p => p.WinnerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}