using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TarefaIngrediente");

            migrationBuilder.DropTable(
                name: "TarefaTecnica");

            migrationBuilder.DropTable(
                name: "TarefaUtensilio");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Tarefa");

            migrationBuilder.DropColumn(
                name: "Tempo",
                table: "Tarefa");

            migrationBuilder.DropColumn(
                name: "Tempo",
                table: "Processo");

            migrationBuilder.AddColumn<string>(
                name: "Texto",
                table: "Tarefa",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Texto",
                table: "Tarefa");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Tarefa",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Tempo",
                table: "Tarefa",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Tempo",
                table: "Processo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TarefaIngrediente",
                columns: table => new
                {
                    TarefaId = table.Column<int>(nullable: false),
                    IngredienteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarefaIngrediente", x => new { x.TarefaId, x.IngredienteId });
                    table.UniqueConstraint("AK_TarefaIngrediente_IngredienteId_TarefaId", x => new { x.IngredienteId, x.TarefaId });
                    table.ForeignKey(
                        name: "FK_TarefaIngrediente_Ingrediente_IngredienteId",
                        column: x => x.IngredienteId,
                        principalTable: "Ingrediente",
                        principalColumn: "IngredienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TarefaIngrediente_Tarefa_TarefaId",
                        column: x => x.TarefaId,
                        principalTable: "Tarefa",
                        principalColumn: "TarefaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TarefaTecnica",
                columns: table => new
                {
                    TarefaId = table.Column<int>(nullable: false),
                    TecnicaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarefaTecnica", x => new { x.TarefaId, x.TecnicaId });
                    table.ForeignKey(
                        name: "FK_TarefaTecnica_Tarefa_TarefaId",
                        column: x => x.TarefaId,
                        principalTable: "Tarefa",
                        principalColumn: "TarefaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TarefaTecnica_Tecnica_TecnicaId",
                        column: x => x.TecnicaId,
                        principalTable: "Tecnica",
                        principalColumn: "TecnicaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TarefaUtensilio",
                columns: table => new
                {
                    TarefaId = table.Column<int>(nullable: false),
                    UtensilioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarefaUtensilio", x => new { x.TarefaId, x.UtensilioId });
                    table.ForeignKey(
                        name: "FK_TarefaUtensilio_Tarefa_TarefaId",
                        column: x => x.TarefaId,
                        principalTable: "Tarefa",
                        principalColumn: "TarefaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TarefaUtensilio_Utensilio_UtensilioId",
                        column: x => x.UtensilioId,
                        principalTable: "Utensilio",
                        principalColumn: "UtensilioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TarefaTecnica_TecnicaId",
                table: "TarefaTecnica",
                column: "TecnicaId");

            migrationBuilder.CreateIndex(
                name: "IX_TarefaUtensilio_UtensilioId",
                table: "TarefaUtensilio",
                column: "UtensilioId");
        }
    }
}
