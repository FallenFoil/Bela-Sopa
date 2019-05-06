using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models.Assistente {
    public class Processo{

        public Processo()
        {
            this.Tarefa = new List<Tarefa>();
        }

        [Key]
        public int ProcessoId { set; get; }

        [Required]
        public int Tempo { set; get; }

        [NotMapped]
        public virtual ICollection<Tarefa> Tarefa { get; set; }
    }

    /*
    public class ProcessoContext : DbContext{
        public ProcessoContext(DbContextOptions<TarefaContext> options)
            : base(options){

        }

        public DbSet<Processo> Processos { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Tarefa>()
            //   .HasMany<Ingrediente>
            //modelBuilder.Entity<Tarefa>()
            //   .HasKey(t => t.Id);
            //modelBuilder.Entity<Ingrediente>().HasKey(i => i.IngredienteId);
            base.OnModelCreating(modelBuilder);
        }
    }
    */
}
