using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddShopSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Icon",
                table: "Categories",
                newName: "LightIcon");

            migrationBuilder.AddColumn<string>(
                name: "ActiveIcon",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DarkIcon",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DaysOfWeek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaysOfWeek", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DayOfWeekTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DayOfWeekId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayOfWeekTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayOfWeekTranslations_DaysOfWeek_DayOfWeekId",
                        column: x => x.DayOfWeekId,
                        principalTable: "DaysOfWeek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayOfWeekTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShopScheduleItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsWeekend = table.Column<bool>(type: "bit", nullable: false),
                    DayOfWeekId = table.Column<int>(type: "int", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopScheduleItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopScheduleItems_DaysOfWeek_DayOfWeekId",
                        column: x => x.DayOfWeekId,
                        principalTable: "DaysOfWeek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopScheduleItems_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayOfWeekTranslations_DayOfWeekId_LanguageId",
                table: "DayOfWeekTranslations",
                columns: new[] { "DayOfWeekId", "LanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DayOfWeekTranslations_LanguageId",
                table: "DayOfWeekTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopScheduleItems_DayOfWeekId",
                table: "ShopScheduleItems",
                column: "DayOfWeekId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopScheduleItems_ShopId",
                table: "ShopScheduleItems",
                column: "ShopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayOfWeekTranslations");

            migrationBuilder.DropTable(
                name: "ShopScheduleItems");

            migrationBuilder.DropTable(
                name: "DaysOfWeek");

            migrationBuilder.DropColumn(
                name: "ActiveIcon",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DarkIcon",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "LightIcon",
                table: "Categories",
                newName: "Icon");
        }
    }
}
