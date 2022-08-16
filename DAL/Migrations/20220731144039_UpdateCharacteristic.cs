using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class UpdateCharacteristic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CharacteristicValues",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CharacteristicNames",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicValues_UserId",
                table: "CharacteristicValues",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicNames_UserId",
                table: "CharacteristicNames",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacteristicNames_AspNetUsers_UserId",
                table: "CharacteristicNames",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacteristicValues_AspNetUsers_UserId",
                table: "CharacteristicValues",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacteristicNames_AspNetUsers_UserId",
                table: "CharacteristicNames");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacteristicValues_AspNetUsers_UserId",
                table: "CharacteristicValues");

            migrationBuilder.DropIndex(
                name: "IX_CharacteristicValues_UserId",
                table: "CharacteristicValues");

            migrationBuilder.DropIndex(
                name: "IX_CharacteristicNames_UserId",
                table: "CharacteristicNames");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CharacteristicValues");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CharacteristicNames");
        }
    }
}
