using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddCharacteristicValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacteristicNames_Products_ProductId",
                table: "CharacteristicNames");

            migrationBuilder.DropForeignKey(
                name: "FK_FilterValues_Unit_UnitId",
                table: "FilterValues");

            migrationBuilder.DropIndex(
                name: "IX_FilterValues_UnitId",
                table: "FilterValues");

            migrationBuilder.DropIndex(
                name: "IX_CharacteristicNames_ProductId",
                table: "CharacteristicNames");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "FilterValues");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "CharacteristicNames");

            migrationBuilder.CreateTable(
                name: "CharacteristicValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CharacteristicNameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacteristicValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacteristicValues_CharacteristicNames_CharacteristicNameId",
                        column: x => x.CharacteristicNameId,
                        principalTable: "CharacteristicNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacteristicValueProduct",
                columns: table => new
                {
                    CharacteristicValuesId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacteristicValueProduct", x => new { x.CharacteristicValuesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_CharacteristicValueProduct_CharacteristicValues_CharacteristicValuesId",
                        column: x => x.CharacteristicValuesId,
                        principalTable: "CharacteristicValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacteristicValueProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicValueProduct_ProductsId",
                table: "CharacteristicValueProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicValues_CharacteristicNameId",
                table: "CharacteristicValues",
                column: "CharacteristicNameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacteristicValueProduct");

            migrationBuilder.DropTable(
                name: "CharacteristicValues");

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "FilterValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "CharacteristicNames",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilterValues_UnitId",
                table: "FilterValues",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicNames_ProductId",
                table: "CharacteristicNames",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacteristicNames_Products_ProductId",
                table: "CharacteristicNames",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FilterValues_Unit_UnitId",
                table: "FilterValues",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id");
        }
    }
}
