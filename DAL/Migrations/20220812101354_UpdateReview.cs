using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class UpdateReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Disadvantage",
                table: "Reviews",
                newName: "Disadvantages");

            migrationBuilder.RenameColumn(
                name: "Advantage",
                table: "Reviews",
                newName: "Advantages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Disadvantages",
                table: "Reviews",
                newName: "Disadvantage");

            migrationBuilder.RenameColumn(
                name: "Advantages",
                table: "Reviews",
                newName: "Advantage");
        }
    }
}
