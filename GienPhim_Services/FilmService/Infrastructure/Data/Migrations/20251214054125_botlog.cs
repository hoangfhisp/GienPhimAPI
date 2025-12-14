using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GienPhim_Services.FilmService.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class botlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BotSyncLogs",
                columns: table => new
                {
                    BotSyncLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhimName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PhimSlug = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Action = table.Column<int>(type: "int", nullable: false),
                    SyncedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotSyncLogs", x => x.BotSyncLogId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BotSyncLogs");
        }
    }
}
