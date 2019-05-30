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
    [Migration("20190530015224_v5")]
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

                    b.Property<int>("ReceitaId");

                    b.Property<DateTime>("Horario");

                    b.HasKey("ClienteId", "ReceitaId");

                    b.HasIndex("ReceitaId");

                    b.ToTable("ClienteEmentaSemanal");
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

                    b.Property<string>("Texto")
                        .IsRequired();

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
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("TecnicaId");

                    b.ToTable("Tecnica");
                });

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.Utensilio", b =>
                {
                    b.Property<int>("UtensilioId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(20);

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

            modelBuilder.Entity("BelaSopa.Models.DomainModels.Assistente.UtilizacaoIngrediente", b =>
                {
                    b.HasOne("BelaSopa.Models.DomainModels.Assistente.Ingrediente", "Ingrediente")
                        .WithMany("Utilizacoes")
                        .HasForeignKey("IngredienteId");

                    b.HasOne("BelaSopa.Models.DomainModels.Assistente.Receita", "Receita")
                        .WithMany("Ingredientes")
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
