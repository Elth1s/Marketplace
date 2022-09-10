using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class UpdateShop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Shops",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "ShopPhone",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shops_CountryId",
                table: "Shops",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Countries_CountryId",
                table: "Shops",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Countries_CountryId",
                table: "Shops");

            migrationBuilder.DropIndex(
                name: "IX_Shops_CountryId",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "ShopPhone");
        }
    }
}
