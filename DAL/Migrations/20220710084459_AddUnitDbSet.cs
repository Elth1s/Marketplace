using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddUnitDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryFilterValue_FilterValues_FiltersValueId",
                table: "CategoryFilterValue");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacteristicNames_Unit_UnitId",
                table: "CharacteristicNames");

            migrationBuilder.DropForeignKey(
                name: "FK_FilterNames_Unit_UnitId",
                table: "FilterNames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Unit",
                table: "Unit");

            migrationBuilder.RenameTable(
                name: "Unit",
                newName: "Units");

            migrationBuilder.RenameColumn(
                name: "FiltersValueId",
                table: "CategoryFilterValue",
                newName: "FilterValuesId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryFilterValue_FiltersValueId",
                table: "CategoryFilterValue",
                newName: "IX_CategoryFilterValue_FilterValuesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Units",
                table: "Units",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryFilterValue_FilterValues_FilterValuesId",
                table: "CategoryFilterValue",
                column: "FilterValuesId",
                principalTable: "FilterValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacteristicNames_Units_UnitId",
                table: "CharacteristicNames",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FilterNames_Units_UnitId",
                table: "FilterNames",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryFilterValue_FilterValues_FilterValuesId",
                table: "CategoryFilterValue");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacteristicNames_Units_UnitId",
                table: "CharacteristicNames");

            migrationBuilder.DropForeignKey(
                name: "FK_FilterNames_Units_UnitId",
                table: "FilterNames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Units",
                table: "Units");

            migrationBuilder.RenameTable(
                name: "Units",
                newName: "Unit");

            migrationBuilder.RenameColumn(
                name: "FilterValuesId",
                table: "CategoryFilterValue",
                newName: "FiltersValueId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryFilterValue_FilterValuesId",
                table: "CategoryFilterValue",
                newName: "IX_CategoryFilterValue_FiltersValueId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Unit",
                table: "Unit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryFilterValue_FilterValues_FiltersValueId",
                table: "CategoryFilterValue",
                column: "FiltersValueId",
                principalTable: "FilterValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacteristicNames_Unit_UnitId",
                table: "CharacteristicNames",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FilterNames_Unit_UnitId",
                table: "FilterNames",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id");
        }
    }
}
