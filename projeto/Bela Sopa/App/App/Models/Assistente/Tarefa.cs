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
            this.TarefaIngrediente = new HashSet<TarefaIngrediente>();
            this.TarefaUtensilio = new HashSet<TarefaUtensilio>();
            this.TarefaTecnica = new HashSet<TarefaTecnica>();
            this.Processo = new HashSet<Processo>();
        }

        [Key]
        public int TarefaId { set; get; }

        [Required]
        [StringLength(200)]
        public string Descricao { set; get; }

        [Required]
        public int Tempo { set; get; }

        [NotMapped]
        public virtual ICollection<TarefaIngrediente> TarefaIngrediente { set; get; }
        [NotMapped]
        public virtual ICollection<TarefaUtensilio> TarefaUtensilio { set; get; }
        [NotMapped]
        public virtual ICollection<TarefaTecnica> TarefaTecnica { set; get; }
        [NotMapped]
        public virtual ICollection<Processo> Processo { set; get; }
    }

    public class TarefaIngrediente {
        [Key]
        public int TarefaId { set; get; }
        [NotMapped]
        public virtual Tarefa Tarefa { set; get; }

        [Key]
        public int IngredienteId { set; get; }
        [NotMapped]
        public virtual Ingrediente Ingrediente {set; get;}
    }

    public class TarefaUtensilio {
        [Key]
        public int TarefaId { set; get; }
        [NotMapped]
        public virtual Tarefa Tarefa { set; get; }

        [Key]
        public int UtensilioId { set; get; }
        [NotMapped]
        public virtual Utensilio Utensilio { set; get; }
    }

    public class TarefaTecnica {
        [Key]
        public int TarefaId { set; get; }
        [NotMapped]
        public virtual Tarefa Tarefa { set; get; }

        [Key]
        public int TecnicaId { set; get; }
        [NotMapped]
        public virtual Tecnica Tecnica { set; get; }
    }

    /*
    public class TarefaContext : DbContext{
        public TarefaContext(DbContextOptions<TarefaContext> options)
            : base(options){

        }

        
        public DbSet<Tarefa> Tarefa { set; get; }
        public DbSet<Ingrediente> Ingrediente { set; get; }
        public DbSet<Utensilio> Utensilio { set; get; }
        public DbSet<Tecnica> Tecnica { set; get; }
        public DbSet<TarefaIngrediente> TarefaIngrediente { get; set; }
        public DbSet<TarefaUtensilio> TarefaUtensilio { get; set; }
        public DbSet<TarefaTecnica> TarefaTecnica { get; set;  }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TarefaIngrediente>()
                .HasKey(ti => new { ti.TarefaId, ti.IngredienteId });
            modelBuilder.Entity<TarefaIngrediente>()
                .HasOne(ti => ti.Tarefa)
                .WithMany(t => t.TarefaIngrediente)
                .HasForeignKey(ti => ti.TarefaId);
            modelBuilder.Entity<TarefaIngrediente>()
                .HasOne(ti => ti.Ingrediente)
                .WithMany(i => i.TarefaIngredientes)
                .HasForeignKey(ti => ti.IngredienteId);

            modelBuilder.Entity<TarefaUtensilio>()
               .HasKey(ti => new { ti.TarefaId, ti.UtensilioId });
            modelBuilder.Entity<TarefaUtensilio>()
                .HasOne(ti => ti.Tarefa)
                .WithMany(t => t.TarefaUtensilio)
                .HasForeignKey(ti => ti.TarefaId);
            modelBuilder.Entity<TarefaUtensilio>()
                .HasOne(ti => ti.Utensilio)
                .WithMany(i => i.TarefaUtensilios)
                .HasForeignKey(ti => ti.UtensilioId);

            modelBuilder.Entity<TarefaTecnica>()
               .HasKey(ti => new { ti.TarefaId, ti.TecnicaId });
            modelBuilder.Entity<TarefaTecnica>()
                .HasOne(ti => ti.Tarefa)
                .WithMany(t => t.TarefaTecnica)
                .HasForeignKey(ti => ti.TarefaId);
            modelBuilder.Entity<TarefaTecnica>()
                .HasOne(ti => ti.Tecnica)
                .WithMany(i => i.TarefaTecnicas)
                .HasForeignKey(ti => ti.TecnicaId);
        }
    }*/
}
