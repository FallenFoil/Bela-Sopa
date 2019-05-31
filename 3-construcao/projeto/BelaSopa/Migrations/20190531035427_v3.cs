using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NomeAlternativoIngrediente",
                columns: table => new
                {
                    NomeAlternativoIngredienteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Valor = table.Column<string>(maxLength: 100, nullable: false),
                    IngredienteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NomeAlternativoIngrediente", x => x.NomeAlternativoIngredienteId);
                    table.ForeignKey(
                        name: "FK_NomeAlternativoIngrediente_Ingrediente_IngredienteId",
                        column: x => x.IngredienteId,
                        principalTable: "Ingrediente",
                        principalColumn: "IngredienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NomeAlternativoTecnica",
                columns: table => new
                {
                    NomeAlternativoTecnicaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Valor = table.Column<string>(maxLength: 100, nullable: false),
                    TecnicaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NomeAlternativoTecnica", x => x.NomeAlternativoTecnicaId);
                    table.ForeignKey(
                        name: "FK_NomeAlternativoTecnica_Tecnica_TecnicaId",
                        column: x => x.TecnicaId,
                        principalTable: "Tecnica",
                        principalColumn: "TecnicaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NomeAlternativoUtensilio",
                columns: table => new
                {
                    NomeAlternativoUtensilioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Valor = table.Column<string>(maxLength: 100, nullable: false),
                    UtensilioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NomeAlternativoUtensilio", x => x.NomeAlternativoUtensilioId);
                    table.ForeignKey(
                        name: "FK_NomeAlternativoUtensilio_Utensilio_UtensilioId",
                        column: x => x.UtensilioId,
                        principalTable: "Utensilio",
                        principalColumn: "UtensilioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NomeAlternativoIngrediente_IngredienteId",
                table: "NomeAlternativoIngrediente",
                column: "IngredienteId");

            migrationBuilder.CreateIndex(
                name: "IX_NomeAlternativoTecnica_TecnicaId",
                table: "NomeAlternativoTecnica",
                column: "TecnicaId");

            migrationBuilder.CreateIndex(
                name: "IX_NomeAlternativoUtensilio_UtensilioId",
                table: "NomeAlternativoUtensilio",
                column: "UtensilioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NomeAlternativoIngrediente");

            migrationBuilder.DropTable(
                name: "NomeAlternativoTecnica");

            migrationBuilder.DropTable(
                name: "NomeAlternativoUtensilio");
        }
    }
}
