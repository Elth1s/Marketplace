using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddSaleTranslation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Sales");

            migrationBuilder.CreateTable(
                name: "SaleTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HorizontalImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerticalImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleTranslations_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleTranslations_LanguageId",
                table: "SaleTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleTranslations_SaleId_LanguageId",
                table: "SaleTranslations",
                columns: new[] { "SaleId", "LanguageId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleTranslations");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
