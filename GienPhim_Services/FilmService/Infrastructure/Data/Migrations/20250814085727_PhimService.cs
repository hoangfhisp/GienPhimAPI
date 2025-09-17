using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GienPhim_Services.FilmService.Data.Migrations
{
    /// <inheritdoc />
    public partial class PhimService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DaoDien",
                columns: table => new
                {
                    DaoDienId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTaDG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaoDien", x => x.DaoDienId);
                });

            migrationBuilder.CreateTable(
                name: "DienVien",
                columns: table => new
                {
                    DienVienId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTaDV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DienVien", x => x.DienVienId);
                });

            migrationBuilder.CreateTable(
                name: "NgonNgu",
                columns: table => new
                {
                    NgonNguId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTaNN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NgonNgu", x => x.NgonNguId);
                });

            migrationBuilder.CreateTable(
                name: "QuocGia",
                columns: table => new
                {
                    QuocGiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenQG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTaQG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuocGia", x => x.QuocGiaId);
                });

            migrationBuilder.CreateTable(
                name: "TheLoai",
                columns: table => new
                {
                    TheLoaiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheLoai", x => x.TheLoaiId);
                });

            migrationBuilder.CreateTable(
                name: "TheTag",
                columns: table => new
                {
                    TheTagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTaT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheTag", x => x.TheTagId);
                });

            migrationBuilder.CreateTable(
                name: "Phim",
                columns: table => new
                {
                    PhimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTaP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuocGiaId = table.Column<int>(type: "int", nullable: false),
                    TenCTSXP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgonNguId = table.Column<int>(type: "int", nullable: false),
                    NgayPhatHanhP = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LuotThichP = table.Column<long>(type: "bigint", nullable: false),
                    LuotDislikeP = table.Column<long>(type: "bigint", nullable: false),
                    LuotXemP = table.Column<long>(type: "bigint", nullable: false),
                    ThuoiLuongP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarPoster = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguonP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTapP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TraiLerP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phim", x => x.PhimId);
                    table.ForeignKey(
                        name: "FK_Phim_NgonNgu_NgonNguId",
                        column: x => x.NgonNguId,
                        principalTable: "NgonNgu",
                        principalColumn: "NgonNguId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Phim_QuocGia_QuocGiaId",
                        column: x => x.QuocGiaId,
                        principalTable: "QuocGia",
                        principalColumn: "QuocGiaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DaoDienCuaPhim",
                columns: table => new
                {
                    PhimId = table.Column<int>(type: "int", nullable: false),
                    DaoDienId = table.Column<int>(type: "int", nullable: false),
                    VaiTroDG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaoDienCuaPhim", x => new { x.PhimId, x.DaoDienId });
                    table.ForeignKey(
                        name: "FK_DaoDienCuaPhim_DaoDien_DaoDienId",
                        column: x => x.DaoDienId,
                        principalTable: "DaoDien",
                        principalColumn: "DaoDienId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DaoDienCuaPhim_Phim_PhimId",
                        column: x => x.PhimId,
                        principalTable: "Phim",
                        principalColumn: "PhimId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DienVienCuaPhim",
                columns: table => new
                {
                    DienVienId = table.Column<int>(type: "int", nullable: false),
                    PhimId = table.Column<int>(type: "int", nullable: false),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DienVienCuaPhim", x => new { x.PhimId, x.DienVienId });
                    table.ForeignKey(
                        name: "FK_DienVienCuaPhim_DienVien_DienVienId",
                        column: x => x.DienVienId,
                        principalTable: "DienVien",
                        principalColumn: "DienVienId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DienVienCuaPhim_Phim_PhimId",
                        column: x => x.PhimId,
                        principalTable: "Phim",
                        principalColumn: "PhimId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhanLoaiTheLoai",
                columns: table => new
                {
                    TheLoaiId = table.Column<int>(type: "int", nullable: false),
                    PhimId = table.Column<int>(type: "int", nullable: false),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanLoaiTheLoai", x => new { x.PhimId, x.TheLoaiId });
                    table.ForeignKey(
                        name: "FK_PhanLoaiTheLoai_Phim_PhimId",
                        column: x => x.PhimId,
                        principalTable: "Phim",
                        principalColumn: "PhimId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhanLoaiTheLoai_TheLoai_TheLoaiId",
                        column: x => x.TheLoaiId,
                        principalTable: "TheLoai",
                        principalColumn: "TheLoaiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhanLoaiTheTag",
                columns: table => new
                {
                    TheTagId = table.Column<int>(type: "int", nullable: false),
                    PhimId = table.Column<int>(type: "int", nullable: false),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanLoaiTheTag", x => new { x.PhimId, x.TheTagId });
                    table.ForeignKey(
                        name: "FK_PhanLoaiTheTag_Phim_PhimId",
                        column: x => x.PhimId,
                        principalTable: "Phim",
                        principalColumn: "PhimId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhanLoaiTheTag_TheTag_TheTagId",
                        column: x => x.TheTagId,
                        principalTable: "TheTag",
                        principalColumn: "TheTagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DaoDienCuaPhim_DaoDienId",
                table: "DaoDienCuaPhim",
                column: "DaoDienId");

            migrationBuilder.CreateIndex(
                name: "IX_DienVienCuaPhim_DienVienId",
                table: "DienVienCuaPhim",
                column: "DienVienId");

            migrationBuilder.CreateIndex(
                name: "IX_PhanLoaiTheLoai_TheLoaiId",
                table: "PhanLoaiTheLoai",
                column: "TheLoaiId");

            migrationBuilder.CreateIndex(
                name: "IX_PhanLoaiTheTag_TheTagId",
                table: "PhanLoaiTheTag",
                column: "TheTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Phim_NgonNguId",
                table: "Phim",
                column: "NgonNguId");

            migrationBuilder.CreateIndex(
                name: "IX_Phim_QuocGiaId",
                table: "Phim",
                column: "QuocGiaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DaoDienCuaPhim");

            migrationBuilder.DropTable(
                name: "DienVienCuaPhim");

            migrationBuilder.DropTable(
                name: "PhanLoaiTheLoai");

            migrationBuilder.DropTable(
                name: "PhanLoaiTheTag");

            migrationBuilder.DropTable(
                name: "DaoDien");

            migrationBuilder.DropTable(
                name: "DienVien");

            migrationBuilder.DropTable(
                name: "TheLoai");

            migrationBuilder.DropTable(
                name: "Phim");

            migrationBuilder.DropTable(
                name: "TheTag");

            migrationBuilder.DropTable(
                name: "NgonNgu");

            migrationBuilder.DropTable(
                name: "QuocGia");
        }
    }
}
