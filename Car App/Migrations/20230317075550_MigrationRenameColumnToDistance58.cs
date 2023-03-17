using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_App.Migrations
{
    /// <inheritdoc />
    public partial class MigrationRenameColumnToDistance58 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<double>(
            //    name: "Distance",
            //    table: "Cars",
            //    type: "float",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<int>(
            //    name: "Distance",
            //    table: "Cars",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(double),
            //    oldType: "float");
        }
    }
}
