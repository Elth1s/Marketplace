using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddDeliveryType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "DeliveryTypeId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DeliveryTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DarkIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LightIcon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryTypeShop",
                columns: table => new
                {
                    DeliveryTypesId = table.Column<int>(type: "int", nullable: false),
                    ShopsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryTypeShop", x => new { x.DeliveryTypesId, x.ShopsId });
                    table.ForeignKey(
                        name: "FK_DeliveryTypeShop_DeliveryTypes_DeliveryTypesId",
                        column: x => x.DeliveryTypesId,
                        principalTable: "DeliveryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryTypeShop_Shops_ShopsId",
                        column: x => x.ShopsId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryTypeTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryTypeId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryTypeTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryTypeTranslations_DeliveryTypes_DeliveryTypeId",
                        column: x => x.DeliveryTypeId,
                        principalTable: "DeliveryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryTypeTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryTypeId",
                table: "Orders",
                column: "DeliveryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryTypeShop_ShopsId",
                table: "DeliveryTypeShop",
                column: "ShopsId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryTypeTranslations_DeliveryTypeId_LanguageId",
                table: "DeliveryTypeTranslations",
                columns: new[] { "DeliveryTypeId", "LanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryTypeTranslations_LanguageId",
                table: "DeliveryTypeTranslations",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryTypes_DeliveryTypeId",
                table: "Orders",
                column: "DeliveryTypeId",
                principalTable: "DeliveryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryTypes_DeliveryTypeId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "DeliveryTypeShop");

            migrationBuilder.DropTable(
                name: "DeliveryTypeTranslations");

            migrationBuilder.DropTable(
                name: "DeliveryTypes");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DeliveryTypeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryTypeId",
                table: "Orders");
        }
    }
}
