using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GienPhim_Services.FilmService.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_lsxfilmview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilmView",
                columns: table => new
                {
                    FilmViewId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhimId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiGianXem = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmView", x => x.FilmViewId);
                    table.ForeignKey(
                        name: "FK_FilmView_Phim_PhimId",
                        column: x => x.PhimId,
                        principalTable: "Phim",
                        principalColumn: "PhimId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LichSuXem",
                columns: table => new
                {
                    LichSuXemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhimId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TapPhimId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TapVuaXem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGianXem = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichSuXem", x => x.LichSuXemId);
                    table.ForeignKey(
                        name: "FK_LichSuXem_Phim_PhimId",
                        column: x => x.PhimId,
                        principalTable: "Phim",
                        principalColumn: "PhimId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilmView_PhimId",
                table: "FilmView",
                column: "PhimId");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuXem_PhimId",
                table: "LichSuXem",
                column: "PhimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmView");

            migrationBuilder.DropTable(
                name: "LichSuXem");
        }
    }
}
