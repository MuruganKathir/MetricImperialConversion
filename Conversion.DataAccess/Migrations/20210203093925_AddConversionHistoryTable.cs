using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Conversion.DataAccess.Migrations
{
    public partial class AddConversionHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConversionHistory",
                columns: table => new
                {
                    HistoryId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConversionType = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    ConversionFrom = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    ConversionTo = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    ValueToConvert = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    ConvertedResult = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversionHistory", x => x.HistoryId);
                    table.ForeignKey(
                        name: "FK_ConversionHistory_User",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConversionHistory_UserId",
                table: "ConversionHistory",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConversionHistory");
        }
    }
}
