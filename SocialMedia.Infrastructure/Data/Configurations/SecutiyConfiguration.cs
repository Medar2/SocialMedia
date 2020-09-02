﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Enumerations;
using System;

namespace SocialMedia.Infrastructure.Data.Configurations
{
    public class SecutiyConfiguration : IEntityTypeConfiguration<Security>
    {
        public void Configure(EntityTypeBuilder<Security> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("Seguridad");

            builder.Property(e => e.Id)
            .HasColumnName("IdSeguridad");

            builder.Property(e => e.User)
            .HasColumnName("Usuairo")
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.UserName)
            .HasColumnName("NombreUsuario")
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Password)
           .HasColumnName("Contasena")
               .IsRequired()
               .HasMaxLength(50)
               .IsUnicode(false);

            builder.Property(e => e.Role)
           .HasColumnName("Rol")
               .IsRequired()
               .HasMaxLength(15)
               .HasConversion(x => x.ToString(),
               x => (RoleType)Enum.Parse(typeof(RoleType), x));
               
        }
    }
}
