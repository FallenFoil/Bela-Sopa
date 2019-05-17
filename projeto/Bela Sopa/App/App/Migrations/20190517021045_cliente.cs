using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class cliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClienteEmentaSemanal",
                columns: table => new
                {
                    ClienteId = table.Column<int>(nullable: false),
                    ReceitaId = table.Column<int>(nullable: false),
                    Horario = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteEmentaSemanal", x => new { x.ClienteId, x.ReceitaId });
                    table.ForeignKey(
                        name: "FK_ClienteEmentaSemanal_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "UtilizadorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClienteEmentaSemanal_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "ReceitaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClienteFavorito",
                columns: table => new
                {
                    ClienteId = table.Column<int>(nullable: false),
                    ReceitaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteFavorito", x => new { x.ClienteId, x.ReceitaId });
                    table.ForeignKey(
                        name: "FK_ClienteFavorito_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "UtilizadorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClienteFavorito_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "ReceitaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClienteFinalizado",
                columns: table => new
                {
                    ClienteId = table.Column<int>(nullable: false),
                    ReceitaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteFinalizado", x => new { x.ClienteId, x.ReceitaId });
                    table.ForeignKey(
                        name: "FK_ClienteFinalizado_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "UtilizadorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClienteFinalizado_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "ReceitaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClienteEmentaSemanal_ReceitaId",
                table: "ClienteEmentaSemanal",
                column: "ReceitaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteFavorito_ReceitaId",
                table: "ClienteFavorito",
                column: "ReceitaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteFinalizado_ReceitaId",
                table: "ClienteFinalizado",
                column: "ReceitaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteEmentaSemanal");

            migrationBuilder.DropTable(
                name: "ClienteFavorito");

            migrationBuilder.DropTable(
                name: "ClienteFinalizado");
        }
    }
}
