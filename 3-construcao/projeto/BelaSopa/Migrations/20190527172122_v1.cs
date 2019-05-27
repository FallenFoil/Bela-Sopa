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
                name: "Administradores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NomeDeUtilizador = table.Column<string>(maxLength: 32, nullable: false),
                    HashPalavraPasse = table.Column<byte[]>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administradores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NomeDeUtilizador = table.Column<string>(maxLength: 32, nullable: false),
                    HashPalavraPasse = table.Column<byte[]>(maxLength: 32, nullable: false),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Etiqueta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etiqueta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingrediente",
                columns: table => new
                {
                    IngredienteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 20, nullable: false),
                    Descricao = table.Column<string>(maxLength: 200, nullable: false),
                    ImagePath = table.Column<string>(maxLength: 50, nullable: false),
                    Link = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingrediente", x => x.IngredienteId);
                });

            migrationBuilder.CreateTable(
                name: "Processo",
                columns: table => new
                {
                    ProcessoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tempo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processo", x => x.ProcessoId);
                });

            migrationBuilder.CreateTable(
                name: "Receita",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    Dificuldade = table.Column<int>(nullable: false),
                    MinutosPreparacao = table.Column<int>(nullable: false),
                    NumDoses = table.Column<int>(nullable: false),
                    Imagem = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receita", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tarefa",
                columns: table => new
                {
                    TarefaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(maxLength: 200, nullable: false),
                    Tempo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefa", x => x.TarefaId);
                });

            migrationBuilder.CreateTable(
                name: "Tecnica",
                columns: table => new
                {
                    TecnicaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 20, nullable: false),
                    Descricao = table.Column<string>(maxLength: 200, nullable: false),
                    ImagePath = table.Column<string>(maxLength: 50, nullable: false),
                    Link = table.Column<string>(maxLength: 50, nullable: false)
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
                    Nome = table.Column<string>(maxLength: 20, nullable: false),
                    Descricao = table.Column<string>(maxLength: 200, nullable: false),
                    ImagePath = table.Column<string>(maxLength: 50, nullable: false),
                    Link = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utensilio", x => x.UtensilioId);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceitaEtiqueta_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceitaIngrediente",
                columns: table => new
                {
                    ReceitaId = table.Column<int>(nullable: false),
                    IngredienteId = table.Column<int>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceitaIngrediente", x => new { x.ReceitaId, x.IngredienteId });
                    table.UniqueConstraint("AK_ReceitaIngrediente_IngredienteId_ReceitaId", x => new { x.IngredienteId, x.ReceitaId });
                    table.ForeignKey(
                        name: "FK_ReceitaIngrediente_Ingrediente_IngredienteId",
                        column: x => x.IngredienteId,
                        principalTable: "Ingrediente",
                        principalColumn: "IngredienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceitaIngrediente_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "Id",
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_ProcessoTarefa_TarefaId",
                table: "ProcessoTarefa",
                column: "TarefaId");

            migrationBuilder.CreateIndex(
                name: "IX_TarefaTecnica_TecnicaId",
                table: "TarefaTecnica",
                column: "TecnicaId");

            migrationBuilder.CreateIndex(
                name: "IX_TarefaUtensilio_UtensilioId",
                table: "TarefaUtensilio",
                column: "UtensilioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administradores");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "ProcessoTarefa");

            migrationBuilder.DropTable(
                name: "ReceitaEtiqueta");

            migrationBuilder.DropTable(
                name: "ReceitaIngrediente");

            migrationBuilder.DropTable(
                name: "ReceitaProcesso");

            migrationBuilder.DropTable(
                name: "TarefaIngrediente");

            migrationBuilder.DropTable(
                name: "TarefaTecnica");

            migrationBuilder.DropTable(
                name: "TarefaUtensilio");

            migrationBuilder.DropTable(
                name: "Etiqueta");

            migrationBuilder.DropTable(
                name: "Processo");

            migrationBuilder.DropTable(
                name: "Receita");

            migrationBuilder.DropTable(
                name: "Ingrediente");

            migrationBuilder.DropTable(
                name: "Tecnica");

            migrationBuilder.DropTable(
                name: "Tarefa");

            migrationBuilder.DropTable(
                name: "Utensilio");
        }
    }
}
