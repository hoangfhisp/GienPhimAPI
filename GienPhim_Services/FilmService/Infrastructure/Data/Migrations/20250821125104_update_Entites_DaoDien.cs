using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GienPhim_Services.FilmService.Data.Migrations
{
    /// <inheritdoc />
    public partial class update_Entites_DaoDien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenDG",
                table: "DaoDien",
                newName: "TenDD");

            migrationBuilder.RenameColumn(
                name: "MoTaDG",
                table: "DaoDien",
                newName: "MoTaDD");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenDD",
                table: "DaoDien",
                newName: "TenDG");

            migrationBuilder.RenameColumn(
                name: "MoTaDD",
                table: "DaoDien",
                newName: "MoTaDG");
        }
    }
}
