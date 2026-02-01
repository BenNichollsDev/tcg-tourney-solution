using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
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

        public virtual DbSet<Player> Players { get; set; }

        public virtual DbSet<Pairing> Pairings { get; set; }

        public virtual DbSet<Match> Matches { get; set; }

        public virtual DbSet<Staff> Staff { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
