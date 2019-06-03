using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Receita",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receita_ClienteId",
                table: "Receita",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receita_Cliente_ClienteId",
                table: "Receita",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "UtilizadorId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receita_Cliente_ClienteId",
                table: "Receita");

            migrationBuilder.DropIndex(
                name: "IX_Receita_ClienteId",
                table: "Receita");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Receita");
        }
    }
}
