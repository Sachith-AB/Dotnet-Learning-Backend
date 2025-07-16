using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dotnet_backend.Migrations
{
    /// <inheritdoc />
    public partial class seedrole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "839e6864-fa20-4aea-b23f-a1d4fe94e5aa", null, "User", "USER" },
                    { "a3c4de77-0596-43ec-9a7c-0db3414e37a3", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "839e6864-fa20-4aea-b23f-a1d4fe94e5aa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a3c4de77-0596-43ec-9a7c-0db3414e37a3");
        }
    }
}
