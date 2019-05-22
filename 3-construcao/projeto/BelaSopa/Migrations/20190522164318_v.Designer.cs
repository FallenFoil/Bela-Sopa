﻿// <auto-generated />
using System;
using BelaSopa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BelaSopa.Migrations
{
    [DbContext(typeof(BelaSopaDbContext))]
    [Migration("20190522164318_v")]
    partial class v
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BelaSopa.Models.Administrador", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("HashPalavraChave")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("NomeDeUtilizador")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Administrador");
                });

            modelBuilder.Entity("BelaSopa.Models.Assistente.Etiqueta", b =>
                {
                    b.Property<int>("EtiquetaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("EtiquetaId");

                    b.ToTable("Etiqueta");
                });

            modelBuilder.Entity("BelaSopa.Models.Assistente.Ingrediente", b =>
                {
                    b.Property<int>("IngredienteId")
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

                    b.HasKey("IngredienteId");

                    b.ToTable("Ingrediente");
                });

            modelBuilder.Entity("BelaSopa.Models.Assistente.Processo", b =>
                {
                    b.Property<int>("ProcessoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Tempo");

                    b.HasKey("ProcessoId");

                    b.ToTable("Processo");
                });

            modelBuilder.Entity("BelaSopa.Models.Assistente.ProcessoTarefa", b =>
                {
                    b.Property<int>("ProcessoId");

                    b.Property<int>("TarefaId");

                    b.HasKey("ProcessoId", "TarefaId");

                    b.HasIndex("TarefaId");

                    b.ToTable("ProcessoTarefa");
                });

            modelBuilder.Entity("BelaSopa.Models.Assistente.Receita", b =>
                {
                    b.Property<int>("ReceitaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Dificuldade")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("NPessoas");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<int>("Tempo");

                    b.Property<string>("Video")
                        .HasMaxLength(50);

                    b.HasKey("ReceitaId");

                    b.ToTable("Receita");
                });

            modelBuilder.Entity("BelaSopa.Models.Assistente.ReceitaEtiqueta", b =>
                {
                    b.Property<int>("ReceitaId");

                    b.Property<int>("EtiquetaId");

                    b.HasKey("ReceitaId", "EtiquetaId");

                    b.HasAlternateKey("EtiquetaId", "ReceitaId");

                    b.ToTable("ReceitaEtiqueta");
                });

            modelBuilder.Entity("BelaSopa.Models.Assistente.ReceitaIngrediente", b =>
                {
                    b.Property<int>("ReceitaId");

                    b.Property<int>("IngredienteId");

                    b.Property<int>("Quantidade");

                    b.HasKey("ReceitaId", "IngredienteId");

                    b.HasAlternateKey("IngredienteId", "ReceitaId");

                    b.ToTable("ReceitaIngrediente");
                });

            modelBuilder.Entity("BelaSopa.Models.Assistente.ReceitaProcesso", b =>
                {
                    b.Property<int>("ReceitaId");

                    b.Property<int>("ProcessoId");

                    b.HasKey("ReceitaId", "ProcessoId");

                    b.HasAlternateKey("ProcessoId", "ReceitaId");

                    b.ToTable("ReceitaProcesso");
                });

            modelBuilder.Entity("BelaSopa.Models.Assistente.Tarefa", b =>
                {
                    b.Property<int>("TarefaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("Tempo");

                    b.HasKey("TarefaId");

                    b.ToTable("Tarefa");
                });

            modelBuilder.Entity("BelaSopa.Models.Assistente.TarefaIngrediente", b =>
                {
                    b.Property<int>("TarefaId");

                    b.Property<int>("IngredienteId");

                    b.HasKey("TarefaId", "IngredienteId");

                    b.HasAlternateKey("IngredienteId", "TarefaId");

                    b.ToTable("TarefaIngrediente");
                });

            modelBuilder.Entity("BelaSopa.Models.Assistente.TarefaTecnica", b =>
                {
                    b.Property<int>("TarefaId");

                    b.Property<int>("TecnicaId");

                    b.HasKey("TarefaId", "TecnicaId");

                    b.HasIndex("TecnicaId");

                    b.ToTable("TarefaTecnica");
                });

            modelBuilder.Entity("BelaSopa.Models.Assistente.TarefaUtensilio", b =>
                {
                    b.Property<int>("TarefaId");

                    b.Property<int>("UtensilioId");

                    b.HasKey("TarefaId", "UtensilioId");

                    b.HasIndex("UtensilioId");

                    b.ToTable("TarefaUtensilio");
                });

            modelBuilder.Entity("BelaSopa.Models.Assistente.Tecnica", b =>
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

            modelBuilder.Entity("BelaSopa.Models.Assistente.Utensilio", b =>
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

            modelBuilder.Entity("BelaSopa.Models.Utilizadores.Cliente", b =>
                {
                    b.Property<int>("UtilizadorId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Distrito")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Localização");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.HasKey("UtilizadorId");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("BelaSopa.Models.Utilizadores.ClienteEmentaSemanal", b =>
                {
                    b.Property<int>("ClienteId");

                    b.Property<int>("ReceitaId");

                    b.Property<DateTime>("Horario");

                    b.HasKey("ClienteId", "ReceitaId");

                    b.HasIndex("ReceitaId");

                    b.ToTable("ClienteEmentaSemanal");
                });

            modelBuilder.Entity("BelaSopa.Models.Utilizadores.ClienteFavorito", b =>
                {
                    b.Property<int>("ClienteId");

                    b.Property<int>("ReceitaId");

                    b.HasKey("ClienteId", "ReceitaId");

                    b.HasIndex("ReceitaId");

                    b.ToTable("ClienteFavorito");
                });

            modelBuilder.Entity("BelaSopa.Models.Utilizadores.ClienteFinalizado", b =>
                {
                    b.Property<int>("ClienteId");

                    b.Property<int>("ReceitaId");

                    b.HasKey("ClienteId", "ReceitaId");

                    b.HasIndex("ReceitaId");

                    b.ToTable("ClienteFinalizado");
                });

            modelBuilder.Entity("BelaSopa.Models.Assistente.ProcessoTarefa", b =>
                {
                    b.HasOne("BelaSopa.Models.Assistente.Processo", "Processo")
                        .WithMany("ProcessoTarefa")
                        .HasForeignKey("ProcessoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BelaSopa.Models.Assistente.Tarefa", "Tarefa")
                        .WithMany("ProcessoTarefa")
                        .HasForeignKey("TarefaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BelaSopa.Models.Assistente.ReceitaEtiqueta", b =>
                {
                    b.HasOne("BelaSopa.Models.Assistente.Etiqueta", "Etiqueta")
                        .WithMany("ReceitaEtiqueta")
                        .HasForeignKey("EtiquetaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BelaSopa.Models.Assistente.Receita", "Receita")
                        .WithMany("ReceitaEtiqueta")
                        .HasForeignKey("ReceitaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BelaSopa.Models.Assistente.ReceitaIngrediente", b =>
                {
                    b.HasOne("BelaSopa.Models.Assistente.Ingrediente", "Ingrediente")
                        .WithMany("ReceitaIngrediente")
                        .HasForeignKey("IngredienteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BelaSopa.Models.Assistente.Receita", "Receita")
                        .WithMany("ReceitaIngrediente")
                        .HasForeignKey("ReceitaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BelaSopa.Models.Assistente.ReceitaProcesso", b =>
                {
                    b.HasOne("BelaSopa.Models.Assistente.Processo", "Processo")
                        .WithMany("ReceitaProcesso")
                        .HasForeignKey("ProcessoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BelaSopa.Models.Assistente.Receita", "Receita")
                        .WithMany("ReceitaProcesso")
                        .HasForeignKey("ReceitaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BelaSopa.Models.Assistente.TarefaIngrediente", b =>
                {
                    b.HasOne("BelaSopa.Models.Assistente.Ingrediente", "Ingrediente")
                        .WithMany("TarefaIngrediente")
                        .HasForeignKey("IngredienteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BelaSopa.Models.Assistente.Tarefa", "Tarefa")
                        .WithMany("TarefaIngrediente")
                        .HasForeignKey("TarefaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BelaSopa.Models.Assistente.TarefaTecnica", b =>
                {
                    b.HasOne("BelaSopa.Models.Assistente.Tarefa", "Tarefa")
                        .WithMany("TarefaTecnica")
                        .HasForeignKey("TarefaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BelaSopa.Models.Assistente.Tecnica", "Tecnica")
                        .WithMany("TarefaTecnica")
                        .HasForeignKey("TecnicaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BelaSopa.Models.Assistente.TarefaUtensilio", b =>
                {
                    b.HasOne("BelaSopa.Models.Assistente.Tarefa", "Tarefa")
                        .WithMany("TarefaUtensilio")
                        .HasForeignKey("TarefaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BelaSopa.Models.Assistente.Utensilio", "Utensilio")
                        .WithMany("TarefaUtensilio")
                        .HasForeignKey("UtensilioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BelaSopa.Models.Utilizadores.ClienteEmentaSemanal", b =>
                {
                    b.HasOne("BelaSopa.Models.Utilizadores.Cliente", "Cliente")
                        .WithMany("ClienteEmentaSemanal")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BelaSopa.Models.Assistente.Receita", "Receita")
                        .WithMany("ClienteEmentaSemanal")
                        .HasForeignKey("ReceitaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BelaSopa.Models.Utilizadores.ClienteFavorito", b =>
                {
                    b.HasOne("BelaSopa.Models.Utilizadores.Cliente", "Cliente")
                        .WithMany("ClienteFavorito")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BelaSopa.Models.Assistente.Receita", "Receita")
                        .WithMany("ClienteFavorito")
                        .HasForeignKey("ReceitaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BelaSopa.Models.Utilizadores.ClienteFinalizado", b =>
                {
                    b.HasOne("BelaSopa.Models.Utilizadores.Cliente", "Cliente")
                        .WithMany("ClienteFinalizado")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BelaSopa.Models.Assistente.Receita", "Receita")
                        .WithMany("ClienteFinalizado")
                        .HasForeignKey("ReceitaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
