using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GienPhim_Services.FilmService.Data.Migrations
{
    /// <inheritdoc />
    public partial class PhimService_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TapPhim",
                columns: table => new
                {
                    TapId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhimId = table.Column<int>(type: "int", nullable: false),
                    TenTap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SlugTap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkChinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkPhu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TapPhim", x => x.TapId);
                    table.ForeignKey(
                        name: "FK_TapPhim_Phim_PhimId",
                        column: x => x.PhimId,
                        principalTable: "Phim",
                        principalColumn: "PhimId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TapPhim_PhimId",
                table: "TapPhim",
                column: "PhimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TapPhim");
        }
    }
}
