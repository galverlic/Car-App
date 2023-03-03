using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_App.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Owners_Cars_CarId",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Owners_CarId",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Owners");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Cars",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Owners_Id",
                table: "Cars",
                column: "Id",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Owners_Id",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Cars");

            migrationBuilder.AddColumn<Guid>(
                name: "CarId",
                table: "Owners",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Owners_CarId",
                table: "Owners",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_Cars_CarId",
                table: "Owners",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
