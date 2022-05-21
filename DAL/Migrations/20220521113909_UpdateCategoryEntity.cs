using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class UpdateCategoryEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Characteristics_CharacteristicId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "CategoryFilterGroup");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CharacteristicId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CharacteristicId",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "CategoryCharacteristic",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    CharacteristicsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryCharacteristic", x => new { x.CategoriesId, x.CharacteristicsId });
                    table.ForeignKey(
                        name: "FK_CategoryCharacteristic_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryCharacteristic_Characteristics_CharacteristicsId",
                        column: x => x.CharacteristicsId,
                        principalTable: "Characteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryFilter",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    FiltersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryFilter", x => new { x.CategoriesId, x.FiltersId });
                    table.ForeignKey(
                        name: "FK_CategoryFilter_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryFilter_Filter_FiltersId",
                        column: x => x.FiltersId,
                        principalTable: "Filter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryCharacteristic_CharacteristicsId",
                table: "CategoryCharacteristic",
                column: "CharacteristicsId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFilter_FiltersId",
                table: "CategoryFilter",
                column: "FiltersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryCharacteristic");

            migrationBuilder.DropTable(
                name: "CategoryFilter");

            migrationBuilder.AddColumn<int>(
                name: "CharacteristicId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoryFilterGroup",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    FilterGroupsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryFilterGroup", x => new { x.CategoriesId, x.FilterGroupsId });
                    table.ForeignKey(
                        name: "FK_CategoryFilterGroup_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryFilterGroup_FilterGroups_FilterGroupsId",
                        column: x => x.FilterGroupsId,
                        principalTable: "FilterGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CharacteristicId",
                table: "Categories",
                column: "CharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFilterGroup_FilterGroupsId",
                table: "CategoryFilterGroup",
                column: "FilterGroupsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Characteristics_CharacteristicId",
                table: "Categories",
                column: "CharacteristicId",
                principalTable: "Characteristics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
