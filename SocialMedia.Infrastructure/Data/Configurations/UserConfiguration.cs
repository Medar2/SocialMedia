using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.UserId);

            builder.ToTable("Usuario");

            builder.Property(e => e.UserId)
            .HasColumnName("IdUsuario");

            builder.Property(e => e.Lastname)
            .HasColumnName("Apellido")
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.DateOfBirth)
            .HasColumnName("FechaNacimiento")
            .HasColumnType("date");

            builder.Property(e => e.IsActive)
            .HasColumnName("Activo");

            builder.Property(e => e.FirstName)
            .HasColumnName("Nombre")
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Telephone)
            .HasColumnName("Telefono")
                .HasMaxLength(10)
                .IsUnicode(false);
        }
    }
}
