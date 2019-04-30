using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models.Assistente{
    public class Receita{
        [Key]
        public int Id;

        [Required]
        [StringLength(10)]
        public string Nome { get; set; }

        [Required]
        [StringLength(10)]
        public string Dificuldade {get; set; }

        [Required]
        public int Tempo {get; set; }
        
        [Required]
        [StringLength(200)]
        public string Descricao {get; set; }

        [StringLength(50)]
        public string Video {get; set; }

        //valores nutricionais
        [Required]
        public int NPessoas {get; set; }

        [Required]
        [StringLength(50)]
        public string Link {get; set; }

        [Required]
        public virtual List<string> Etiquetas { get; set; }

        [Required]
        public virtual List<Processo> Processos {get; set; }

        [Required]
        public virtual Dictionary<string, Ingrediente> Ingredientes {get; set; }

        [Required]
        public virtual Dictionary<string, int> Quantidades {get; set; }

        [Required]
        public virtual List<Utensilio> Utensilios {get; set; }

        [Required]
        public virtual List<Tecnica> Tecnicas {get; set; }
    }

    /*
    public class ReceitaContext : DbContext{
        public ReceitaContext(DbContextOptions<ReceitaContext> options)
            : base(options){

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Por fazer
        }


        public DbSet<Receita> receitas { get; set; }
        //public DbSet<Models.Task> task { get; set; }
    }*/
}
