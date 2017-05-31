using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoDoc.DAL.Migrations
{
    public partial class addedproperytobookmarkentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Bookmarks",
                newName: "MessageJson");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Bookmarks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Bookmarks");

            migrationBuilder.RenameColumn(
                name: "MessageJson",
                table: "Bookmarks",
                newName: "Message");
        }
    }
}
