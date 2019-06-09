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
                    Texto = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingrediente", x => x.IngredienteId);
                });

            migrationBuilder.CreateTable(
                name: "Tecnica",
                columns: table => new
                {
                    TecnicaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    Texto = table.Column<string>(nullable: false)
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
                    Texto = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utensilio", x => x.UtensilioId);
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
                    ClienteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receita", x => x.ReceitaId);
                    table.ForeignKey(
                        name: "FK_Receita_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "UtilizadorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClienteExcluiIngrediente",
                columns: table => new
                {
                    ClienteId = table.Column<int>(nullable: false),
                    IngredienteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteExcluiIngrediente", x => new { x.ClienteId, x.IngredienteId });
                    table.ForeignKey(
                        name: "FK_ClienteExcluiIngrediente_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "UtilizadorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClienteExcluiIngrediente_Ingrediente_IngredienteId",
                        column: x => x.IngredienteId,
                        principalTable: "Ingrediente",
                        principalColumn: "IngredienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NomeAlternativoIngrediente",
                columns: table => new
                {
                    NomeAlternativoIngredienteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
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
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
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
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
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

            migrationBuilder.CreateTable(
                name: "ClienteReceitaFavorita",
                columns: table => new
                {
                    ClienteId = table.Column<int>(nullable: false),
                    ReceitaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteReceitaFavorita", x => new { x.ClienteId, x.ReceitaId });
                    table.ForeignKey(
                        name: "FK_ClienteReceitaFavorita_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "UtilizadorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClienteReceitaFavorita_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "ReceitaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClienteReceitaFinalizada",
                columns: table => new
                {
                    ClienteId = table.Column<int>(nullable: false),
                    ReceitaId = table.Column<int>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: false),
                    AvaliacaoDificuldade = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteReceitaFinalizada", x => new { x.ClienteId, x.ReceitaId, x.DataInicio });
                    table.UniqueConstraint("AK_ClienteReceitaFinalizada_ClienteId_DataInicio_ReceitaId", x => new { x.ClienteId, x.DataInicio, x.ReceitaId });
                    table.ForeignKey(
                        name: "FK_ClienteReceitaFinalizada_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "UtilizadorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClienteReceitaFinalizada_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "ReceitaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstadoConfecao",
                columns: table => new
                {
                    NumProcesso = table.Column<int>(nullable: false),
                    Inicio = table.Column<DateTime>(nullable: false),
                    ClienteId = table.Column<int>(nullable: false),
                    ReceitaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoConfecao", x => x.ClienteId);
                    table.ForeignKey(
                        name: "FK_EstadoConfecao_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "UtilizadorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstadoConfecao_Receita_ReceitaId",
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
                    Indice = table.Column<int>(nullable: false),
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
                    Nome = table.Column<string>(nullable: true),
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
                    Indice = table.Column<int>(nullable: false),
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

            migrationBuilder.CreateTable(
                name: "TextoTarefa",
                columns: table => new
                {
                    TextoTarefaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Indice = table.Column<int>(nullable: false),
                    Texto = table.Column<string>(nullable: false),
                    IngredienteId = table.Column<int>(nullable: true),
                    TecnicaId = table.Column<int>(nullable: true),
                    UtensilioId = table.Column<int>(nullable: true),
                    TarefaId = table.Column<int>(nullable: false)
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
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_ClienteExcluiIngrediente_IngredienteId",
                table: "ClienteExcluiIngrediente",
                column: "IngredienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteReceitaFavorita_ReceitaId",
                table: "ClienteReceitaFavorita",
                column: "ReceitaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteReceitaFinalizada_ReceitaId",
                table: "ClienteReceitaFinalizada",
                column: "ReceitaId");

            migrationBuilder.CreateIndex(
                name: "IX_EstadoConfecao_ReceitaId",
                table: "EstadoConfecao",
                column: "ReceitaId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Processo_ReceitaId",
                table: "Processo",
                column: "ReceitaId");

            migrationBuilder.CreateIndex(
                name: "IX_Receita_ClienteId",
                table: "Receita",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_RefeicaoEmentaSemanal_ReceitaId",
                table: "RefeicaoEmentaSemanal",
                column: "ReceitaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefa_ProcessoId",
                table: "Tarefa",
                column: "ProcessoId");

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
                name: "ClienteExcluiIngrediente");

            migrationBuilder.DropTable(
                name: "ClienteReceitaFavorita");

            migrationBuilder.DropTable(
                name: "ClienteReceitaFinalizada");

            migrationBuilder.DropTable(
                name: "EstadoConfecao");

            migrationBuilder.DropTable(
                name: "NomeAlternativoIngrediente");

            migrationBuilder.DropTable(
                name: "NomeAlternativoTecnica");

            migrationBuilder.DropTable(
                name: "NomeAlternativoUtensilio");

            migrationBuilder.DropTable(
                name: "ReceitaEtiqueta");

            migrationBuilder.DropTable(
                name: "RefeicaoEmentaSemanal");

            migrationBuilder.DropTable(
                name: "TextoTarefa");

            migrationBuilder.DropTable(
                name: "UtilizacaoIngrediente");

            migrationBuilder.DropTable(
                name: "ValorNutricional");

            migrationBuilder.DropTable(
                name: "Etiqueta");

            migrationBuilder.DropTable(
                name: "Tarefa");

            migrationBuilder.DropTable(
                name: "Tecnica");

            migrationBuilder.DropTable(
                name: "Utensilio");

            migrationBuilder.DropTable(
                name: "Ingrediente");

            migrationBuilder.DropTable(
                name: "Processo");

            migrationBuilder.DropTable(
                name: "Receita");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
