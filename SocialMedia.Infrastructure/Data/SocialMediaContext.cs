using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SocialMedia.Core.Entities;
using SocialMedia.Infrastructure.Data.Configurations;

namespace SocialMedia.Infrastructure.Data
{
    public partial class SocialMediaContext : DbContext
    {
        public SocialMediaContext()
        {
        }

        public SocialMediaContext(DbContextOptions<SocialMediaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<User> Users { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
////            if (!optionsBuilder.IsConfigured)
////            {
////#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
////                optionsBuilder.UseSqlServer("Server=DEVISS-03-JC\\SQLXP2017;Database=SocialMedia;User Id=administrador;Password=DevSolutions3;MultipleActiveResultSets=true");
////            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            #region OldCode
            //modelBuilder.Entity<Comment>(entity =>
            //{
            //    entity.ToTable("Comentatio");
            //    entity.HasKey(e => e.CommentId);

            //    entity.Property(e => e.CommentId)
            //    .HasColumnName("IdComentario")
            //    .ValueGeneratedNever();

            //    entity.Property(e => e.Description)
            //    .HasColumnName("Descripcion")
            //    .IsRequired()
            //        .HasMaxLength(500)
            //        .IsUnicode(false);

            //    entity.Property(e => e.PostId)
            //    .HasColumnName("IdPublicacion");

            //    entity.Property(e => e.UserId)
            //    .HasColumnName("IdUsuario");

            //    entity.Property(e => e.IsActive)
            //    .HasColumnName("Activo");

            //    entity.Property(e => e.Date)
            //    .HasColumnName("Fecha")
            //    .HasColumnType("datetime");


            //    entity.HasOne(d => d.Post)
            //        .WithMany(p => p.Comments)
            //        .HasForeignKey(d => d.PostId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Comentario_Publicacion");

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.Comments)
            //        .HasForeignKey(d => d.UserId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Comentario_Usuario");
            //});

            //modelBuilder.Entity<Post>(entity =>
            //{
            //    entity.HasKey(e => e.Postid);
            //    entity.ToTable("Publicacion");

            //    entity.Property(e => e.Postid)
            //    .HasColumnName("IdPublicacion");

            //    entity.Property(e => e.UserId)
            //    .HasColumnName("IdUsuario");

            //    entity.Property(e => e.Description)
            //    .HasColumnName("Descripcion")
            //        .IsRequired()
            //        .HasMaxLength(1000)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Date)
            //    .HasColumnName("Fecha")
            //    .HasColumnType("datetime");

            //    entity.Property(e => e.Image)
            //    .HasColumnName("Imagen")    
            //    .HasMaxLength(500)
            //        .IsUnicode(false);

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.Post)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Publicacion_Usuario");
            //});
            //modelBuilder.Entity<User>(entity =>
            //{
            //    entity.HasKey(e => e.UserId);

            //    entity.ToTable("Usuario");

            //    entity.Property(e => e.UserId)
            //    .HasColumnName("IdUsuario");

            //    entity.Property(e => e.Lastname)
            //    .HasColumnName("Apellido")
            //        .IsRequired()
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Email)
            //        .IsRequired()
            //        .HasMaxLength(30)
            //        .IsUnicode(false);

            //    entity.Property(e => e.DateOfBirth)
            //    .HasColumnName("FechaNacimiento")
            //    .HasColumnType("date");

            //    entity.Property(e => e.IsActive)
            //    .HasColumnName("Activo");

            //    entity.Property(e => e.FirstName)
            //    .HasColumnName("Nombre")
            //        .IsRequired()
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Telephone)
            //    .HasColumnName("Telefono")
            //        .HasMaxLength(10)
            //        .IsUnicode(false);
            //});

            //OnModelCreatingPartial(modelBuilder);
            #endregion

            modelBuilder.ApplyConfiguration(new PostConfigurations());

           
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
