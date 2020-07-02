﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Data.Configurations
{
    public class PostConfigurations : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
             builder.HasKey(e => e.Postid);
                builder.ToTable("Publicacion");

                builder.Property(e => e.Postid)
                .HasColumnName("IdPublicacion");

                builder.Property(e => e.UserId)
                .HasColumnName("IdUsuario");

                builder.Property(e => e.Description)
                .HasColumnName("Descripcion")
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                builder.Property(e => e.Date)
                .HasColumnName("Fecha")
                .HasColumnType("datetime");

                builder.Property(e => e.Image)
                .HasColumnName("Imagen")    
                .HasMaxLength(500)
                    .IsUnicode(false);

                builder.HasOne(d => d.User)
                    .WithMany(p => p.Post)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Publicacion_Usuario");
        }
    }
}