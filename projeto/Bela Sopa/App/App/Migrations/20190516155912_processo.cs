using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class processo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessoTarefa",
                columns: table => new
                {
                    ProcessoId = table.Column<int>(nullable: false),
                    TarefaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessoTarefa", x => new { x.ProcessoId, x.TarefaId });
                    table.ForeignKey(
                        name: "FK_ProcessoTarefa_Processo_ProcessoId",
                        column: x => x.ProcessoId,
                        principalTable: "Processo",
                        principalColumn: "ProcessoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessoTarefa_Tarefa_TarefaId",
                        column: x => x.TarefaId,
                        principalTable: "Tarefa",
                        principalColumn: "TarefaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessoTarefa_TarefaId",
                table: "ProcessoTarefa",
                column: "TarefaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessoTarefa");
        }
    }
}
