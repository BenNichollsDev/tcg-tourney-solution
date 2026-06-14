/*
Program: Local Games Store Management System
Filename: AppDbContext.cs
Author: Benjamin Nicholls
Course: BSc Software Engineering (Hons)
Module: CSY4022 - Computing Project Dissertation
Module Leader: Amir Minai
Supervisor: Mark Johnson

Date: 14/06/2026

Disclaimer: The following source code is the sole work of the author unless otherwise stated.
Copyright (C) Benjamin Nicholls. All Rights Reserved.
*/
using Microsoft.EntityFrameworkCore;
using TCG.Domain.Entities;
using TCG.Infrastructure.Configurations;

namespace TCG.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<League> Leagues { get; set; } = null!;

        public virtual DbSet<Tournament> Tournaments { get; set; } = null!;

        public virtual DbSet<Pairing> Pairings { get; set; } = null!;

        public virtual DbSet<Staff> Staff { get; set; } = null!;

        public virtual DbSet<TournamentPlayer> TournamentPlayers { get; set; } = null!;

        public virtual DbSet<Player> Player { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.ApplyConfiguration(new TournamentConfiguration());
            modelBuilder.ApplyConfiguration(new TournamentPlayerConfiguration());
            modelBuilder.ApplyConfiguration(new PairingConfiguration());
            modelBuilder.ApplyConfiguration(new StaffConfiguration());
            modelBuilder.ApplyConfiguration(new LeagueConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerConfiguration());

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
