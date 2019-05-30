using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceitaIngrediente");

            migrationBuilder.CreateTable(
                name: "UtilizacaoIngrediente",
                columns: table => new
                {
                    ReceitaIngredienteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Quantidade = table.Column<string>(maxLength: 50, nullable: false),
                    ReceitaId = table.Column<int>(nullable: false),
                    IngredienteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilizacaoIngrediente", x => x.ReceitaIngredienteId);
                    table.ForeignKey(
                        name: "FK_UtilizacaoIngrediente_Ingrediente_IngredienteId",
                        column: x => x.IngredienteId,
                        principalTable: "Ingrediente",
                        principalColumn: "IngredienteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UtilizacaoIngrediente_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "ReceitaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UtilizacaoIngrediente_IngredienteId",
                table: "UtilizacaoIngrediente",
                column: "IngredienteId");

            migrationBuilder.CreateIndex(
                name: "IX_UtilizacaoIngrediente_ReceitaId",
                table: "UtilizacaoIngrediente",
                column: "ReceitaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UtilizacaoIngrediente");

            migrationBuilder.CreateTable(
                name: "ReceitaIngrediente",
                columns: table => new
                {
                    ReceitaIngredienteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IngredienteId = table.Column<int>(nullable: true),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Quantidade = table.Column<string>(maxLength: 50, nullable: false),
                    ReceitaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceitaIngrediente", x => x.ReceitaIngredienteId);
                    table.ForeignKey(
                        name: "FK_ReceitaIngrediente_Ingrediente_IngredienteId",
                        column: x => x.IngredienteId,
                        principalTable: "Ingrediente",
                        principalColumn: "IngredienteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceitaIngrediente_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "ReceitaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaIngrediente_IngredienteId",
                table: "ReceitaIngrediente",
                column: "IngredienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaIngrediente_ReceitaId",
                table: "ReceitaIngrediente",
                column: "ReceitaId");
        }
    }
}
