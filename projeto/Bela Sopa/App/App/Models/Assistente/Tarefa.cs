using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models.Assistente{
    public class Tarefa{
        [Key]
        public int Id;

        [Required]
        [StringLength(200)]
        public string Descricao;

        [Required]
        public int Tempo;

        public virtual Dictionary<int, Ingrediente> Ingredientes { set; get; }
        public virtual Dictionary<int, Utensilio> Utensilios { set; get; }
        public virtual Dictionary<int, Tecnica> Tecnicas { set; get; }
    }

    public class TarefaContext : DbContext
    {
        public TarefaContext(DbContextOptions<TarefaContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Tarefa>()
             //   .HasMany<Ingrediente>
                
        }

        public DbSet<Tarefa> Tarefa { get; set; }
        public DbSet<Ingrediente> Ingrediente { get; set; }
    }
}
