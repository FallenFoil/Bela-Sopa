using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteEmentaSemanal");

            migrationBuilder.DropTable(
                name: "DataRefeicao");

            migrationBuilder.CreateTable(
                name: "RefeicaoEmentaSemanal",
                columns: table => new
                {
                    ClienteId = table.Column<int>(nullable: false),
                    DiaDaSemana = table.Column<int>(nullable: false),
                    RefeicaoDoDia = table.Column<int>(nullable: false),
                    ReceitaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefeicaoEmentaSemanal", x => new { x.ClienteId, x.DiaDaSemana, x.RefeicaoDoDia });
                    table.ForeignKey(
                        name: "FK_RefeicaoEmentaSemanal_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "UtilizadorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RefeicaoEmentaSemanal_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "ReceitaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefeicaoEmentaSemanal_ReceitaId",
                table: "RefeicaoEmentaSemanal",
                column: "ReceitaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefeicaoEmentaSemanal");

            migrationBuilder.CreateTable(
                name: "ClienteEmentaSemanal",
                columns: table => new
                {
                    ClienteId = table.Column<int>(nullable: false),
                    DataRefeicaoId = table.Column<int>(nullable: false),
                    ReceitaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteEmentaSemanal", x => new { x.ClienteId, x.DataRefeicaoId });
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
                name: "DataRefeicao",
                columns: table => new
                {
                    DataRefeicaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Almoco = table.Column<bool>(nullable: false),
                    Dia = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataRefeicao", x => x.DataRefeicaoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClienteEmentaSemanal_ReceitaId",
                table: "ClienteEmentaSemanal",
                column: "ReceitaId");
        }
    }
}
