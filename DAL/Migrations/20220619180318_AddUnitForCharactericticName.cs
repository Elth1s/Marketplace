using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddUnitForCharactericticName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacteristicProduct");

            migrationBuilder.DropTable(
                name: "Characteristics");

            migrationBuilder.CreateTable(
                name: "CharacteristicNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CharacteristicGroupId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacteristicNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacteristicNames_CharacteristicGroups_CharacteristicGroupId",
                        column: x => x.CharacteristicGroupId,
                        principalTable: "CharacteristicGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacteristicNames_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CharacteristicNames_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicNames_CharacteristicGroupId",
                table: "CharacteristicNames",
                column: "CharacteristicGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicNames_ProductId",
                table: "CharacteristicNames",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicNames_UnitId",
                table: "CharacteristicNames",
                column: "UnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacteristicNames");

            migrationBuilder.CreateTable(
                name: "Characteristics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacteristicGroupId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characteristics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characteristics_CharacteristicGroups_CharacteristicGroupId",
                        column: x => x.CharacteristicGroupId,
                        principalTable: "CharacteristicGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacteristicProduct",
                columns: table => new
                {
                    CharacteristicsId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacteristicProduct", x => new { x.CharacteristicsId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_CharacteristicProduct_Characteristics_CharacteristicsId",
                        column: x => x.CharacteristicsId,
                        principalTable: "Characteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacteristicProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicProduct_ProductsId",
                table: "CharacteristicProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_CharacteristicGroupId",
                table: "Characteristics",
                column: "CharacteristicGroupId");
        }
    }
}
