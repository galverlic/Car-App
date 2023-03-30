using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_App.Migrations
{
    /// <inheritdoc />
    public partial class DropColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "Mileage",
               table: "Cars");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // not needed

        }
    }
}
