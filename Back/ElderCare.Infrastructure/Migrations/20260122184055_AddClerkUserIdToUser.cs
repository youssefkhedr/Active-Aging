using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElderCare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClerkUserIdToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClerkUserId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d"),
                columns: new[] { "ClerkUserId", "CreatedAt", "ProfilePictureUrl" },
                values: new object[] { null, new DateTime(2026, 1, 22, 18, 40, 53, 791, DateTimeKind.Utc).AddTicks(384), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClerkUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 17, 54, 47, 783, DateTimeKind.Utc).AddTicks(1719));
        }
    }
}
