using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessoTarefa");

            migrationBuilder.DropTable(
                name: "ReceitaProcesso");

            migrationBuilder.AddColumn<int>(
                name: "ProcessoId",
                table: "Tarefa",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReceitaId",
                table: "Processo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tarefa_ProcessoId",
                table: "Tarefa",
                column: "ProcessoId");

            migrationBuilder.CreateIndex(
                name: "IX_Processo_ReceitaId",
                table: "Processo",
                column: "ReceitaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Processo_Receita_ReceitaId",
                table: "Processo",
                column: "ReceitaId",
                principalTable: "Receita",
                principalColumn: "ReceitaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefa_Processo_ProcessoId",
                table: "Tarefa",
                column: "ProcessoId",
                principalTable: "Processo",
                principalColumn: "ProcessoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processo_Receita_ReceitaId",
                table: "Processo");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarefa_Processo_ProcessoId",
                table: "Tarefa");

            migrationBuilder.DropIndex(
                name: "IX_Tarefa_ProcessoId",
                table: "Tarefa");

            migrationBuilder.DropIndex(
                name: "IX_Processo_ReceitaId",
                table: "Processo");

            migrationBuilder.DropColumn(
                name: "ProcessoId",
                table: "Tarefa");

            migrationBuilder.DropColumn(
                name: "ReceitaId",
                table: "Processo");

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

            migrationBuilder.CreateTable(
                name: "ReceitaProcesso",
                columns: table => new
                {
                    ReceitaId = table.Column<int>(nullable: false),
                    ProcessoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceitaProcesso", x => new { x.ReceitaId, x.ProcessoId });
                    table.UniqueConstraint("AK_ReceitaProcesso_ProcessoId_ReceitaId", x => new { x.ProcessoId, x.ReceitaId });
                    table.ForeignKey(
                        name: "FK_ReceitaProcesso_Processo_ProcessoId",
                        column: x => x.ProcessoId,
                        principalTable: "Processo",
                        principalColumn: "ProcessoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceitaProcesso_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "ReceitaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessoTarefa_TarefaId",
                table: "ProcessoTarefa",
                column: "TarefaId");
        }
    }
}
