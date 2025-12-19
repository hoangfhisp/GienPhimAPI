using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GienPhim_Services.MembershipService.Data.Migrations
{
    /// <inheritdoc />
    public partial class Membership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MembershipPlan",
                columns: table => new
                {
                    PlanId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenPlan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanSlug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTaPlan = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipPlan", x => x.PlanId);
                });

            migrationBuilder.CreateTable(
                name: "PlanOption",
                columns: table => new
                {
                    OptionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlanId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ThoiGianSuDung = table.Column<int>(type: "int", nullable: false),
                    GiaOption = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanOption", x => x.OptionId);
                    table.ForeignKey(
                        name: "FK_PlanOption_MembershipPlan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "MembershipPlan",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMembership",
                columns: table => new
                {
                    UserMembershipId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OptionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMembership", x => x.UserMembershipId);
                    table.ForeignKey(
                        name: "FK_UserMembership_PlanOption_OptionId",
                        column: x => x.OptionId,
                        principalTable: "PlanOption",
                        principalColumn: "OptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanOption_PlanId",
                table: "PlanOption",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMembership_OptionId",
                table: "UserMembership",
                column: "OptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMembership");

            migrationBuilder.DropTable(
                name: "PlanOption");

            migrationBuilder.DropTable(
                name: "MembershipPlan");
        }
    }
}
