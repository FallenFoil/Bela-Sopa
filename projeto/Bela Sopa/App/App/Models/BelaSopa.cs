using App.Models.Assistente;
using App.Models.Utilizadores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models {
    public class BelaSopa {
        Receita Receita;
        Dictionary<int, Dictionary<int, Boolean>> EstadoConfecao;
    }

    public class BelaSopaContext : DbContext {
        public BelaSopaContext(DbContextOptions<BelaSopaContext> options)
            : base(options) {

        }

        public DbSet<Tarefa> Tarefa { set; get; }
        public DbSet<Ingrediente> Ingrediente { set; get; }
        public DbSet<Utensilio> Utensilio { set; get; }
        public DbSet<Tecnica> Tecnica { set; get; }
        public DbSet<Processo> Processo { set; get; }
        public DbSet<Cliente> Cliente { get; set; } 
        public DbSet<Administrador> Administrador { get; set; }
        public DbSet<TarefaIngrediente> TarefaIngrediente { get; set; }
        public DbSet<TarefaUtensilio> TarefaUtensilio { get; set; }
        public DbSet<TarefaTecnica> TarefaTecnica { get; set; } 


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
           // modelBuilder.Entity<Ingrediente>().ToTable("Ingrediente");

            modelBuilder.Entity<TarefaIngrediente>()
                .HasKey(ti => new { ti.TarefaId, ti.IngredienteId });
            modelBuilder.Entity<TarefaIngrediente>()
                .HasOne(ti => ti.Tarefa)
                .WithMany(t => t.TarefaIngrediente)
                .HasForeignKey(ti => ti.TarefaId);
            modelBuilder.Entity<TarefaIngrediente>()
                .HasOne(ti => ti.Ingrediente)
                .WithMany(i => i.TarefaIngrediente)
                .HasForeignKey(ti => ti.IngredienteId);

            modelBuilder.Entity<TarefaUtensilio>()
               .HasKey(ti => new { ti.TarefaId, ti.UtensilioId });
            modelBuilder.Entity<TarefaUtensilio>()
                .HasOne(ti => ti.Tarefa)
                .WithMany(t => t.TarefaUtensilio)
                .HasForeignKey(ti => ti.TarefaId);
            modelBuilder.Entity<TarefaUtensilio>()
                .HasOne(ti => ti.Utensilio)
                .WithMany(i => i.TarefaUtensilio)
                .HasForeignKey(ti => ti.UtensilioId);

            modelBuilder.Entity<TarefaTecnica>()
               .HasKey(ti => new { ti.TarefaId, ti.TecnicaId });
            modelBuilder.Entity<TarefaTecnica>()
                .HasOne(ti => ti.Tarefa)
                .WithMany(t => t.TarefaTecnica)
                .HasForeignKey(ti => ti.TarefaId);
            modelBuilder.Entity<TarefaTecnica>()
                .HasOne(ti => ti.Tecnica)
                .WithMany(i => i.TarefaTecnica)
                .HasForeignKey(ti => ti.TecnicaId);
        }
    }
}
