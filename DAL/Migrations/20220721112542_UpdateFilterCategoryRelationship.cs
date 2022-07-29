using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class UpdateFilterCategoryRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_FilterNames_FilterNameId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_FilterNameId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "FilterNameId",
                table: "Categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "FilterNameId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_FilterNameId",
                table: "Categories",
                column: "FilterNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_FilterNames_FilterNameId",
                table: "Categories",
                column: "FilterNameId",
                principalTable: "FilterNames",
                principalColumn: "Id");
        }
    }
}
