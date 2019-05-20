using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class receita2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Etiqueta",
                columns: table => new
                {
                    EtiquetaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etiqueta", x => x.EtiquetaId);
                });

            migrationBuilder.CreateTable(
                name: "ReceitaEtiqueta",
                columns: table => new
                {
                    EtiquetaId = table.Column<int>(nullable: false),
                    ReceitaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceitaEtiqueta", x => new { x.ReceitaId, x.EtiquetaId });
                    table.UniqueConstraint("AK_ReceitaEtiqueta_EtiquetaId_ReceitaId", x => new { x.EtiquetaId, x.ReceitaId });
                    table.ForeignKey(
                        name: "FK_ReceitaEtiqueta_Etiqueta_EtiquetaId",
                        column: x => x.EtiquetaId,
                        principalTable: "Etiqueta",
                        principalColumn: "EtiquetaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceitaEtiqueta_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "ReceitaId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceitaEtiqueta");

            migrationBuilder.DropTable(
                name: "Etiqueta");
        }
    }
}
