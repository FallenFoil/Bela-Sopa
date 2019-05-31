// <auto-generated />
using System;
using BelaSopa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BelaSopa.Migrations
{
    [DbContext(typeof(BelaSopaContext))]
    [Migration("20190531083253_v5")]
    partial class v5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.ClienteEmentaSemanal", b =>
                {
                    b.Property<int>("ClienteId");

                    b.Property<int>("DataRefeicaoId");

                    b.Property<int>("ReceitaId");

                    b.HasKey("ClienteId", "DataRefeicaoId");

                    b.HasIndex("ReceitaId");

                    b.ToTable("ClienteEmentaSemanal");
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.ClienteFavorito", b =>
                {
                    b.Property<int>("ClienteId");

                    b.Property<int>("ReceitaId");

                    b.HasKey("ClienteId", "ReceitaId");

                    b.HasIndex("ReceitaId");

                    b.ToTable("ClienteFavorito");
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.ClienteFinalizado", b =>
                {
                    b.Property<int>("ClienteId");

                    b.Property<int>("ReceitaId");

                    b.Property<DateTime>("Data");

                    b.HasKey("ClienteId", "ReceitaId");

                    b.HasIndex("ReceitaId");

                    b.ToTable("ClienteFinalizado");
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.DataRefeicao", b =>
                {
                    b.Property<int>("DataRefeicaoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Almoco");

                    b.Property<int>("Dia");

                    b.HasKey("DataRefeicaoId");

                    b.ToTable("DataRefeicao");
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.Etiqueta", b =>
                {
                    b.Property<int>("EtiquetaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("EtiquetaId");

                    b.ToTable("Etiqueta");
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.Ingrediente", b =>
                {
                    b.Property<int>("IngredienteId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.Property<byte[]>("Imagem")
                        .IsRequired();

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Texto")
                        .IsRequired();

                    b.HasKey("IngredienteId");

                    b.ToTable("Ingrediente");
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.NomeAlternativoIngrediente", b =>
                {
                    b.Property<int>("NomeAlternativoIngredienteId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IngredienteId");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("NomeAlternativoIngredienteId");

                    b.HasIndex("IngredienteId");

                    b.ToTable("NomeAlternativoIngrediente");
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.NomeAlternativoTecnica", b =>
                {
                    b.Property<int>("NomeAlternativoTecnicaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("TecnicaId");

                    b.HasKey("NomeAlternativoTecnicaId");

                    b.HasIndex("TecnicaId");

                    b.ToTable("NomeAlternativoTecnica");
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.NomeAlternativoUtensilio", b =>
                {
                    b.Property<int>("NomeAlternativoUtensilioId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("UtensilioId");

                    b.HasKey("NomeAlternativoUtensilioId");

                    b.HasIndex("UtensilioId");

                    b.ToTable("NomeAlternativoUtensilio");
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.Processo", b =>
                {
                    b.Property<int>("ProcessoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ReceitaId");

                    b.HasKey("ProcessoId");

                    b.HasIndex("ReceitaId");

                    b.ToTable("Processo");
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.Receita", b =>
                {
                    b.Property<int>("ReceitaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.Property<int>("Dificuldade");

                    b.Property<byte[]>("Imagem")
                        .IsRequired();

                    b.Property<int>("MinutosPreparacao");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("NumeroDoses");

                    b.HasKey("ReceitaId");

                    b.ToTable("Receita");
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.ReceitaEtiqueta", b =>
                {
                    b.Property<int>("ReceitaId");

                    b.Property<int>("EtiquetaId");

                    b.HasKey("ReceitaId", "EtiquetaId");

                    b.HasAlternateKey("EtiquetaId", "ReceitaId");

                    b.ToTable("ReceitaEtiqueta");
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.Tarefa", b =>
                {
                    b.Property<int>("TarefaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProcessoId");

                    b.HasKey("TarefaId");

                    b.HasIndex("ProcessoId");

                    b.ToTable("Tarefa");
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.Tecnica", b =>
                {
                    b.Property<int>("TecnicaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.Property<byte[]>("Imagem")
                        .IsRequired();

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Texto")
                        .IsRequired();

                    b.HasKey("TecnicaId");

                    b.ToTable("Tecnica");
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.TextoTarefa", b =>
                {
                    b.Property<int>("TextoTarefaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("IngredienteId");

                    b.Property<int>("TarefaId");

                    b.Property<int?>("TecnicaId");

                    b.Property<string>("Texto")
                        .IsRequired();

                    b.Property<int?>("UtensilioId");

                    b.HasKey("TextoTarefaId");

                    b.HasIndex("IngredienteId");

                    b.HasIndex("TarefaId");

                    b.HasIndex("TecnicaId");

                    b.HasIndex("UtensilioId");

                    b.ToTable("TextoTarefa");
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.Utensilio", b =>
                {
                    b.Property<int>("UtensilioId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.Property<byte[]>("Imagem")
                        .IsRequired();

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Texto")
                        .IsRequired();

                    b.HasKey("UtensilioId");

                    b.ToTable("Utensilio");
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.UtilizacaoIngrediente", b =>
                {
                    b.Property<int>("ReceitaIngredienteId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("IngredienteId");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Quantidade")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("ReceitaId");

                    b.HasKey("ReceitaIngredienteId");

                    b.HasIndex("IngredienteId");

                    b.HasIndex("ReceitaId");

                    b.ToTable("UtilizacaoIngrediente");
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.ValorNutricional", b =>
                {
                    b.Property<int>("ValorNutricionalId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Dose")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("PercentagemVdrAdulto");

                    b.Property<int>("ReceitaId");

                    b.HasKey("ValorNutricionalId");

                    b.HasIndex("ReceitaId");

                    b.ToTable("ValorNutricional");
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Utilizadores.Administrador", b =>
                {
                    b.Property<int>("UtilizadorId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("HashPalavraPasse")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("NomeDeUtilizador")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("UtilizadorId");

                    b.ToTable("Administrador");
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Utilizadores.Cliente", b =>
                {
                    b.Property<int>("UtilizadorId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<byte[]>("HashPalavraPasse")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("NomeDeUtilizador")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("UtilizadorId");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.ClienteEmentaSemanal", b =>
                {
                    b.HasOne("BelaSopa.Models.DomainModels.Utilizadores.Cliente", "Cliente")
                        .WithMany("ClienteEmentaSemanal")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BelaSopa.Models.DomainModels.Assistente.Receita", "Receita")
                        .WithMany("ClienteEmentaSemanal")
                        .HasForeignKey("ReceitaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.ClienteFavorito", b =>
                {
                    b.HasOne("BelaSopa.Models.DomainModels.Utilizadores.Cliente", "Cliente")
                        .WithMany("ClienteFavorito")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BelaSopa.Models.DomainModels.Assistente.Receita", "Receita")
                        .WithMany("ClienteFavorito")
                        .HasForeignKey("ReceitaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.ClienteFinalizado", b =>
                {
                    b.HasOne("BelaSopa.Models.DomainModels.Utilizadores.Cliente", "Cliente")
                        .WithMany("ClienteFinalizado")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BelaSopa.Models.DomainModels.Assistente.Receita", "Receita")
                        .WithMany("ClienteFinalizado")
                        .HasForeignKey("ReceitaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.NomeAlternativoIngrediente", b =>
                {
                    b.HasOne("BelaSopa.Models.DomainModels.Assistente.Ingrediente", "Ingrediente")
                        .WithMany("NomesAlternativos")
                        .HasForeignKey("IngredienteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.NomeAlternativoTecnica", b =>
                {
                    b.HasOne("BelaSopa.Models.DomainModels.Assistente.Tecnica", "Tecnica")
                        .WithMany("NomesAlternativos")
                        .HasForeignKey("TecnicaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.NomeAlternativoUtensilio", b =>
                {
                    b.HasOne("BelaSopa.Models.DomainModels.Assistente.Utensilio", "Utensilio")
                        .WithMany("NomesAlternativos")
                        .HasForeignKey("UtensilioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.Processo", b =>
                {
                    b.HasOne("BelaSopa.Models.DomainModels.Assistente.Receita", "Receita")
                        .WithMany("Processos")
                        .HasForeignKey("ReceitaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.ReceitaEtiqueta", b =>
                {
                    b.HasOne("BelaSopa.Models.DomainModels.Assistente.Etiqueta", "Etiqueta")
                        .WithMany("ReceitaEtiqueta")
                        .HasForeignKey("EtiquetaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BelaSopa.Models.DomainModels.Assistente.Receita", "Receita")
                        .WithMany("ReceitaEtiqueta")
                        .HasForeignKey("ReceitaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.Tarefa", b =>
                {
                    b.HasOne("BelaSopa.Models.DomainModels.Assistente.Processo", "Processo")
                        .WithMany("Tarefas")
                        .HasForeignKey("ProcessoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.TextoTarefa", b =>
                {
                    b.HasOne("BelaSopa.Models.DomainModels.Assistente.Ingrediente", "Ingrediente")
                        .WithMany()
                        .HasForeignKey("IngredienteId");

                    b.HasOne("BelaSopa.Models.DomainModels.Assistente.Tarefa", "Tarefa")
                        .WithMany("Texto")
                        .HasForeignKey("TarefaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BelaSopa.Models.DomainModels.Assistente.Tecnica", "Tecnica")
                        .WithMany()
                        .HasForeignKey("TecnicaId");

                    b.HasOne("BelaSopa.Models.DomainModels.Assistente.Utensilio", "Utensilio")
                        .WithMany()
                        .HasForeignKey("UtensilioId");
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.UtilizacaoIngrediente", b =>
                {
                    b.HasOne("BelaSopa.Models.DomainModels.Assistente.Ingrediente", "Ingrediente")
                        .WithMany("Utilizacoes")
                        .HasForeignKey("IngredienteId");

                    b.HasOne("BelaSopa.Models.DomainModels.Assistente.Receita", "Receita")
                        .WithMany("UtilizacoesIngredientes")
                        .HasForeignKey("ReceitaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.ValorNutricional", b =>
                {
                    b.HasOne("BelaSopa.Models.DomainModels.Assistente.Receita", "Receita")
                        .WithMany("ValoresNutricionais")
                        .HasForeignKey("ReceitaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
