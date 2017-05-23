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
            base.OnModelCreating(modelBuilder);
        }
    }
}
