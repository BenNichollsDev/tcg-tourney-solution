using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
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
                   .HasColumnName("staff_id");

            builder.Property(s => s.StaffFirstName)
                   .HasColumnName("staff_first_name");

            builder.Property(s => s.StaffSurname)
                   .HasColumnName("staff_surname");

            builder.Property(s => s.StaffEmail)
                   .HasColumnName("staff_email");

            builder.Property(s => s.StaffMobile)
                   .HasColumnName("staff_mobile");

            builder.Property(s => s.StaffRoleManagement)
                   .HasColumnName("staff_role_management");

            builder.Property(s => s.StaffRoleHead)
                   .HasColumnName("staff_role_head");
        }
    }

}
