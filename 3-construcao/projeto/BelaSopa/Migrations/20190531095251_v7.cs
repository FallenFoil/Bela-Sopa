using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Indice",
                table: "TextoTarefa",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Indice",
                table: "Tarefa",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Indice",
                table: "Processo",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Indice",
                table: "TextoTarefa");

            migrationBuilder.DropColumn(
                name: "Indice",
                table: "Tarefa");

            migrationBuilder.DropColumn(
                name: "Indice",
                table: "Processo");
        }
    }
}
