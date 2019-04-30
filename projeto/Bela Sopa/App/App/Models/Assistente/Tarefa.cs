using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models.Assistente{
    public class Tarefa{

        public Tarefa(){
            this.TarefaIngredientes = new HashSet<TarefaIngrediente>();
            this.Utensilios = new HashSet<Utensilio>();
            this.Tecnicas = new HashSet<Tecnica>();
            this.Processos = new HashSet<Processo>();
        }

        [Key]
        public int TarefaId { set; get; }

        [Required]
        [StringLength(200)]
        public string Descricao { set; get; }

        [Required]
        public int Tempo { set; get; }

        public ICollection<TarefaIngrediente> TarefaIngredientes { set; get; }
        public virtual ICollection<Utensilio> Utensilios { set; get; }
        public virtual ICollection<Tecnica> Tecnicas { set; get; }
        [NotMapped]
        public virtual ICollection<Processo> Processos { set; get; }
    }

    public class TarefaIngrediente
    {
        [Key]
        public int TarefaId { set; get; }
        public Tarefa Tarefa { set; get; }

        [Key]
        public int IngredienteId { set; get; }
        public Ingrediente Ingrediente {set; get;}
    }

    public class TarefaContext : DbContext{
        public TarefaContext(DbContextOptions<TarefaContext> options)
            : base(options){

        }


        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Ingrediente> Ingredientes { set; get; }
        public DbSet<TarefaIngrediente> TarefaIngredientes { get; set; }
        public DbSet<Utensilio> Utensilios { set; get; }
        public DbSet<Tecnica> Tecnicas { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Tarefa>()
            //   .HasMany<Ingrediente>
            //modelBuilder.Entity<Ingrediente>().HasKey(i => i.IngredienteId);
            modelBuilder.Entity<TarefaIngrediente>().HasKey(ti => new { ti.TarefaId, ti.IngredienteId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
