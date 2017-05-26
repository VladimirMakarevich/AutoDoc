using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AutoDoc.DAL.Context;

namespace AutoDoc.DAL.Migrations
{
    [DbContext(typeof(AutoDocContext))]
    [Migration("20170526135603_updateBookmarkModel")]
    partial class updateBookmarkModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AutoDoc.DAL.Entities.Bookmark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DocumentId");

                    b.Property<string>("MessageJson")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.ToTable("Bookmarks");
                });

            modelBuilder.Entity("AutoDoc.DAL.Entities.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Path");

                    b.HasKey("Id");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("AutoDoc.DAL.Entities.Bookmark", b =>
                {
                    b.HasOne("AutoDoc.DAL.Entities.Document", "Document")
                        .WithMany("Bookmarks")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
