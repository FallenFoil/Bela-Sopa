using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Clientes",
                newName: "UtilizadorId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Administradores",
                newName: "UtilizadorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UtilizadorId",
                table: "Clientes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UtilizadorId",
                table: "Administradores",
                newName: "Id");
        }
    }
}
