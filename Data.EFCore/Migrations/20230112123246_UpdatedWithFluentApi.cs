using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedWithFluentApi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Desks_OwnerId",
                table: "Desks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RentingStart",
                table: "Desks",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RentingEnd",
                table: "Desks",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Desks_OwnerId",
                table: "Desks",
                column: "OwnerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Desks_OwnerId",
                table: "Desks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RentingStart",
                table: "Desks",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RentingEnd",
                table: "Desks",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Desks_OwnerId",
                table: "Desks",
                column: "OwnerId");
        }
    }
}
