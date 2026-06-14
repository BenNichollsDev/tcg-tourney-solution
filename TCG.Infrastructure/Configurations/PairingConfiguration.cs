/*
Program: Local Games Store Management System
Filename: PairingConfiguration.cs
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

            builder.Property(p => p.Player1Id)
                .HasColumnName("pairing_tp_1");

            builder.Property(p => p.Player2Id)
                .HasColumnName("pairing_tp_2");

            builder.Property(p => p.Player1Score)
                .HasColumnName("pairing_tp_1_score");

            builder.Property(p => p.Player2Score)
                .HasColumnName("pairing_tp_2_score");

            builder.Property(p => p.WinnerId)
                .HasColumnName("pairing_winner");

            builder.Property(p => p.PairingDraw)
                .HasColumnName("pairing_draw");

            builder.HasOne(p => p.Player1)
                .WithMany(tp => tp.PairingsAsPlayer1)
                .HasForeignKey(p => p.Player1Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Player2)
                .WithMany(tp => tp.PairingsAsPlayer2)
                .HasForeignKey(p => p.Player2Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Winner)
                .WithMany()
                .HasForeignKey(p => p.WinnerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
