using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GienPhim_Services.FilmService.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_bot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BotConfigs",
                columns: table => new
                {
                    BotConfigId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApiPerRun = table.Column<int>(type: "int", nullable: false),
                    IntervalHours = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotConfigs", x => x.BotConfigId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BotConfigs");
        }
    }
}
