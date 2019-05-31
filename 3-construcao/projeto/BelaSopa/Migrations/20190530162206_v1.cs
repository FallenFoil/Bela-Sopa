using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrador",
                columns: table => new
                {
                    UtilizadorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NomeDeUtilizador = table.Column<string>(maxLength: 32, nullable: false),
                    HashPalavraPasse = table.Column<byte[]>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrador", x => x.UtilizadorId);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    UtilizadorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NomeDeUtilizador = table.Column<string>(maxLength: 32, nullable: false),
                    HashPalavraPasse = table.Column<byte[]>(maxLength: 32, nullable: false),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.UtilizadorId);
                });

            migrationBuilder.CreateTable(
                name: "Etiqueta",
                columns: table => new
                {
                    EtiquetaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etiqueta", x => x.EtiquetaId);
                });

            migrationBuilder.CreateTable(
                name: "Ingrediente",
                columns: table => new
                {
                    IngredienteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    Texto = table.Column<string>(nullable: false),
                    Imagem = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingrediente", x => x.IngredienteId);
                });

            migrationBuilder.CreateTable(
                name: "Receita",
                columns: table => new
                {
                    ReceitaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    Dificuldade = table.Column<int>(nullable: false),
                    MinutosPreparacao = table.Column<int>(nullable: false),
                    NumeroDoses = table.Column<int>(nullable: false),
                    Imagem = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receita", x => x.ReceitaId);
                });

            migrationBuilder.CreateTable(
                name: "Tecnica",
                columns: table => new
                {
                    TecnicaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    Texto = table.Column<string>(nullable: false),
                    Imagem = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tecnica", x => x.TecnicaId);
                });

            migrationBuilder.CreateTable(
                name: "Utensilio",
                columns: table => new
                {
                    UtensilioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    Texto = table.Column<string>(nullable: false),
                    Imagem = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utensilio", x => x.UtensilioId);
                });

            migrationBuilder.CreateTable(
                name: "ClienteEmentaSemanal",
                columns: table => new
                {
                    ClienteId = table.Column<int>(nullable: false),
                    Horario = table.Column<TimeSpan>(nullable: false),
                    ReceitaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteEmentaSemanal", x => new { x.ClienteId, x.Horario });
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
                    ReceitaId = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Processo",
                columns: table => new
                {
                    ProcessoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReceitaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processo", x => x.ProcessoId);
                    table.ForeignKey(
                        name: "FK_Processo_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "ReceitaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceitaEtiqueta",
                columns: table => new
                {
                    ReceitaId = table.Column<int>(nullable: false),
                    EtiquetaId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ValorNutricional",
                columns: table => new
                {
                    ValorNutricionalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    Dose = table.Column<string>(maxLength: 20, nullable: false),
                    PercentagemVdrAdulto = table.Column<int>(nullable: true),
                    ReceitaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValorNutricional", x => x.ValorNutricionalId);
                    table.ForeignKey(
                        name: "FK_ValorNutricional_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "ReceitaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tarefa",
                columns: table => new
                {
                    TarefaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Texto = table.Column<string>(nullable: false),
                    ProcessoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefa", x => x.TarefaId);
                    table.ForeignKey(
                        name: "FK_Tarefa_Processo_ProcessoId",
                        column: x => x.ProcessoId,
                        principalTable: "Processo",
                        principalColumn: "ProcessoId",
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

            migrationBuilder.CreateIndex(
                name: "IX_Processo_ReceitaId",
                table: "Processo",
                column: "ReceitaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefa_ProcessoId",
                table: "Tarefa",
                column: "ProcessoId");

            migrationBuilder.CreateIndex(
                name: "IX_UtilizacaoIngrediente_IngredienteId",
                table: "UtilizacaoIngrediente",
                column: "IngredienteId");

            migrationBuilder.CreateIndex(
                name: "IX_UtilizacaoIngrediente_ReceitaId",
                table: "UtilizacaoIngrediente",
                column: "ReceitaId");

            migrationBuilder.CreateIndex(
                name: "IX_ValorNutricional_ReceitaId",
                table: "ValorNutricional",
                column: "ReceitaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrador");

            migrationBuilder.DropTable(
                name: "ClienteEmentaSemanal");

            migrationBuilder.DropTable(
                name: "ClienteFavorito");

            migrationBuilder.DropTable(
                name: "ClienteFinalizado");

            migrationBuilder.DropTable(
                name: "ReceitaEtiqueta");

            migrationBuilder.DropTable(
                name: "Tarefa");

            migrationBuilder.DropTable(
                name: "Tecnica");

            migrationBuilder.DropTable(
                name: "Utensilio");

            migrationBuilder.DropTable(
                name: "UtilizacaoIngrediente");

            migrationBuilder.DropTable(
                name: "ValorNutricional");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Etiqueta");

            migrationBuilder.DropTable(
                name: "Processo");

            migrationBuilder.DropTable(
                name: "Ingrediente");

            migrationBuilder.DropTable(
                name: "Receita");
        }
    }
}
