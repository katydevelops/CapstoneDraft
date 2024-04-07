using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapstoneDraft.Migrations
{
    public partial class UpdateRequiredNameUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedTimeStamp",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);
        }
    }
}
