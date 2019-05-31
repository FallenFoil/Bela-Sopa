using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Texto",
                table: "Tarefa");

            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "NomeAlternativoUtensilio",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "NomeAlternativoTecnica",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "NomeAlternativoIngrediente",
                newName: "Nome");

            migrationBuilder.CreateTable(
                name: "TextoTarefa",
                columns: table => new
                {
                    TextoTarefaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Texto = table.Column<string>(nullable: false),
                    IngredienteId = table.Column<int>(nullable: true),
                    TecnicaId = table.Column<int>(nullable: true),
                    UtensilioId = table.Column<int>(nullable: true),
                    TarefaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextoTarefa", x => x.TextoTarefaId);
                    table.ForeignKey(
                        name: "FK_TextoTarefa_Ingrediente_IngredienteId",
                        column: x => x.IngredienteId,
                        principalTable: "Ingrediente",
                        principalColumn: "IngredienteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TextoTarefa_Tarefa_TarefaId",
                        column: x => x.TarefaId,
                        principalTable: "Tarefa",
                        principalColumn: "TarefaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TextoTarefa_Tecnica_TecnicaId",
                        column: x => x.TecnicaId,
                        principalTable: "Tecnica",
                        principalColumn: "TecnicaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TextoTarefa_Utensilio_UtensilioId",
                        column: x => x.UtensilioId,
                        principalTable: "Utensilio",
                        principalColumn: "UtensilioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TextoTarefa_IngredienteId",
                table: "TextoTarefa",
                column: "IngredienteId");

            migrationBuilder.CreateIndex(
                name: "IX_TextoTarefa_TarefaId",
                table: "TextoTarefa",
                column: "TarefaId");

            migrationBuilder.CreateIndex(
                name: "IX_TextoTarefa_TecnicaId",
                table: "TextoTarefa",
                column: "TecnicaId");

            migrationBuilder.CreateIndex(
                name: "IX_TextoTarefa_UtensilioId",
                table: "TextoTarefa",
                column: "UtensilioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TextoTarefa");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "NomeAlternativoUtensilio",
                newName: "Valor");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "NomeAlternativoTecnica",
                newName: "Valor");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "NomeAlternativoIngrediente",
                newName: "Valor");

            migrationBuilder.AddColumn<string>(
                name: "Texto",
                table: "Tarefa",
                nullable: false,
                defaultValue: "");
        }
    }
}
