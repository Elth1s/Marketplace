using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class UpdateProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilterValues_Categories_CategoryId",
                table: "FilterValues");

            migrationBuilder.DropIndex(
                name: "IX_FilterValues_CategoryId",
                table: "FilterValues");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "FilterValues");

            migrationBuilder.AddColumn<Guid>(
                name: "UrlSlug",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CategoryFilterValue",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    FiltersValueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryFilterValue", x => new { x.CategoriesId, x.FiltersValueId });
                    table.ForeignKey(
                        name: "FK_CategoryFilterValue_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryFilterValue_FilterValues_FiltersValueId",
                        column: x => x.FiltersValueId,
                        principalTable: "FilterValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFilterValue_FiltersValueId",
                table: "CategoryFilterValue",
                column: "FiltersValueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryFilterValue");

            migrationBuilder.DropColumn(
                name: "UrlSlug",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "FilterValues",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilterValues_CategoryId",
                table: "FilterValues",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_FilterValues_Categories_CategoryId",
                table: "FilterValues",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
