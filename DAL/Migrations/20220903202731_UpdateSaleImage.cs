using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class UpdateSaleImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Sales",
                newName: "VerticalImage");

            migrationBuilder.AddColumn<string>(
                name: "HorizontalImage",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HorizontalImage",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "VerticalImage",
                table: "Sales",
                newName: "Image");
        }
    }
}
