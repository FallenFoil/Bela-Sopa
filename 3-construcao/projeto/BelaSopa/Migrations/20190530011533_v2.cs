using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Horario",
                table: "ClienteEmentaSemanal",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.CreateTable(
                name: "ClientesFavorito",
                columns: table => new
                {
                    ClienteId = table.Column<int>(nullable: false),
                    ReceitaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientesFavorito", x => new { x.ClienteId, x.ReceitaId });
                    table.ForeignKey(
                        name: "FK_ClientesFavorito_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "UtilizadorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientesFavorito_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "ReceitaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientesFinalizado",
                columns: table => new
                {
                    ClienteId = table.Column<int>(nullable: false),
                    ReceitaId = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientesFinalizado", x => new { x.ClienteId, x.ReceitaId });
                    table.ForeignKey(
                        name: "FK_ClientesFinalizado_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "UtilizadorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientesFinalizado_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "ReceitaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientesFavorito_ReceitaId",
                table: "ClientesFavorito",
                column: "ReceitaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientesFinalizado_ReceitaId",
                table: "ClientesFinalizado",
                column: "ReceitaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientesFavorito");

            migrationBuilder.DropTable(
                name: "ClientesFinalizado");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Horario",
                table: "ClienteEmentaSemanal",
                nullable: false,
                oldClrType: typeof(TimeSpan));
        }
    }
}
