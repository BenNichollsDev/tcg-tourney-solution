/*
Program: Local Games Store Management System
Filename: StaffConfiguration.cs
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
    public class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.ToTable("staff");

            builder.HasKey(s => s.StaffId);

            builder.Property(s => s.StaffId)
                .HasColumnName("staff_id")
                .ValueGeneratedOnAdd();

            builder.Property(s => s.StaffFirstName)
                .HasColumnName("staff_first_name")
                .IsRequired();

            builder.Property(s => s.StaffSurname)
                .HasColumnName("staff_surname")
                .IsRequired();

            builder.Property(s => s.StaffEmail)
                .HasColumnName("staff_email")
                .IsRequired();

            // Stored passwords are hashed
            builder.Property(s => s.StaffPassword)
                .HasColumnName("staff_password")
                .IsRequired();

            builder.Property(s => s.StaffMobile)
                .HasColumnName("staff_mobile");
        }
    }
}
