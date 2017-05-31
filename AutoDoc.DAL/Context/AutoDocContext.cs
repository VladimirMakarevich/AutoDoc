using AutoDoc.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AutoDoc.DAL.Context
{
    public class AutoDocContext : DbContext
    {
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Document> Documents { get; set; }

        public AutoDocContext(DbContextOptions<AutoDocContext> options)
            : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bookmark>(entity =>
            {
                entity.HasOne(b => b.Document)
                    .WithMany(d => d.Bookmarks)
                    .HasForeignKey(b => b.DocumentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
