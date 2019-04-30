﻿// <auto-generated />
using System;
using App.Models.Assistente;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.Migrations
{
    [DbContext(typeof(TarefaContext))]
    [Migration("20190430150107_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("App.Models.Assistente.Ingrediente", b =>
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

                    b.ToTable("Ingredientes");
                });

            modelBuilder.Entity("App.Models.Assistente.Tarefa", b =>
                {
                    b.Property<int>("TarefaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("Tempo");

                    b.HasKey("TarefaId");

                    b.ToTable("Tarefas");
                });

            modelBuilder.Entity("App.Models.Assistente.TarefaIngrediente", b =>
                {
                    b.Property<int>("TarefaId");

                    b.Property<int>("IngredienteId");

                    b.HasKey("TarefaId", "IngredienteId");

                    b.HasAlternateKey("IngredienteId", "TarefaId");

                    b.ToTable("TarefaIngredientes");
                });

            modelBuilder.Entity("App.Models.Assistente.Tecnica", b =>
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

                    b.Property<int?>("TarefaId");

                    b.HasKey("TecnicaId");

                    b.HasIndex("TarefaId");

                    b.ToTable("Tecnicas");
                });

            modelBuilder.Entity("App.Models.Assistente.Utensilio", b =>
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

                    b.Property<int?>("TarefaId");

                    b.HasKey("UtensilioId");

                    b.HasIndex("TarefaId");

                    b.ToTable("Utensilios");
                });

            modelBuilder.Entity("App.Models.Assistente.TarefaIngrediente", b =>
                {
                    b.HasOne("App.Models.Assistente.Ingrediente", "Ingrediente")
                        .WithMany("TarefaIngredientes")
                        .HasForeignKey("IngredienteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("App.Models.Assistente.Tarefa", "Tarefa")
                        .WithMany("TarefaIngredientes")
                        .HasForeignKey("TarefaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("App.Models.Assistente.Tecnica", b =>
                {
                    b.HasOne("App.Models.Assistente.Tarefa")
                        .WithMany("Tecnicas")
                        .HasForeignKey("TarefaId");
                });

            modelBuilder.Entity("App.Models.Assistente.Utensilio", b =>
                {
                    b.HasOne("App.Models.Assistente.Tarefa")
                        .WithMany("Utensilios")
                        .HasForeignKey("TarefaId");
                });
#pragma warning restore 612, 618
        }
    }
}
