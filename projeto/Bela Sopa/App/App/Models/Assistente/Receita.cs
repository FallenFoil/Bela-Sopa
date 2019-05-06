using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models.Assistente{
    public class Receita{
        [Key]
        public int ReceitaId { get; set; }

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
        [NotMapped]
        public virtual IList<string> Etiqueta { get; set; }

        [Required]
        [NotMapped]
        public virtual List<Processo> Processo {get; set; }

        [Required]
        [NotMapped]
        public virtual Dictionary<string, Ingrediente> Ingrediente {get; set; }

        [Required]
        [NotMapped]
        public virtual Dictionary<string, int> Quantidade {get; set; }

        [Required]
        [NotMapped]
        public virtual List<Utensilio> Utensilio {get; set; }

        [Required]
        [NotMapped]
        public virtual List<Tecnica> Tecnica {get; set; }
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
