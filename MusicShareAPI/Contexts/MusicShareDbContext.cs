using Microsoft.EntityFrameworkCore;
using MusicShareAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShareAPI.Contexts
{
    public partial class MusicShareDbContext : DbContext
    {

        public MusicShareDbContext()
        {
        }

        public MusicShareDbContext(DbContextOptions<MusicShareDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Songs> Songs { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=musicshare;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<Songs>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AlbumId).HasColumnName("albumId");

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.GenreId).HasColumnName("genreId");

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasColumnName("imageUrl")
                    .HasMaxLength(255);

                entity.Property(e => e.Likes)
                    .HasColumnName("likes")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Plays)
                    .HasColumnName("plays")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.SongUrl)
                    .IsRequired()
                    .HasColumnName("songUrl")
                    .HasMaxLength(255);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updatedAt")
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Available)
                    .HasColumnName("available")
                    .HasDefaultValueSql("true");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
