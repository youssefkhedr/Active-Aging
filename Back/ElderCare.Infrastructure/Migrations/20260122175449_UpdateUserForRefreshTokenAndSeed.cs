using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElderCare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserForRefreshTokenAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiry",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "CreatedAt", "Email", "FullName", "Gender", "IsActive", "PasswordHash", "RefreshToken", "RefreshTokenExpiry", "Role" },
                values: new object[] { new Guid("a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d"), 30, new DateTime(2026, 1, 22, 17, 54, 47, 783, DateTimeKind.Utc).AddTicks(1719), "yousefahmed012732@gmail.com", "Admin User", "Male", true, "$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgNoTfG6YmD8oJ8e.973jR.2W6U.", null, null, "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d"));

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiry",
                table: "Users");
        }
    }
}
