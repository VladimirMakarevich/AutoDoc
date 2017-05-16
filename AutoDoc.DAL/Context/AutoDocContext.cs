using System;
using System.Collections.Generic;
using System.Text;
using AutoDoc.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AutoDoc.DAL.Context
{
    public class AutoDocContext : DbContext
    {
        public AutoDocContext(DbContextOptions<AutoDocContext> options) : base(options) { }

        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bookmark>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(b => b.Document)
                    .WithMany(d => d.Bookmarks)
                    .HasForeignKey(b => b.DocumentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Name).IsRequired();
            });
        }
    }
}
