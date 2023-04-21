using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class updatedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ec35413-f05f-4466-ac25-ddd6d2e7d1ec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b62e30e0-76a1-4265-be51-121970d4c231");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "18c509e6-05f0-47b2-95e1-041f98213e2a", null, "Administrator", "ADMINISTRATOR" },
                    { "64b4e759-4c96-47f3-9f22-004f9642fec9", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18c509e6-05f0-47b2-95e1-041f98213e2a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64b4e759-4c96-47f3-9f22-004f9642fec9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4ec35413-f05f-4466-ac25-ddd6d2e7d1ec", null, "User", "USER" },
                    { "b62e30e0-76a1-4265-be51-121970d4c231", null, "Admin", "ADMINISTRATOR" }
                });
        }
    }
}
