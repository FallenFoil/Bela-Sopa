using App.Models.Utilizadores;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<ReceitaEtiqueta> ReceitaEtiqueta { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<ReceitaProcesso> ReceitaProcesso {get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<ReceitaIngrediente> ReceitaIngrediente {get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<ClienteFavorito> ClienteFavorito { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<ClienteFinalizado> ClienteFinalizado { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<ClienteEmentaSemanal> ClienteEmentaSemanal { get; set; }

        /*
        [Required]
        [NotMapped]
        public virtual List<Utensilio> Utensilio {get; set; }

        [Required]
        [NotMapped]
        public virtual List<Tecnica> Tecnica {get; set; }
        */
    }

    public class Etiqueta {
        [Key]
        public int EtiquetaId { set; get; }
        [Required]
        [StringLength(20)]
        public string Nome { set; get; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<ReceitaEtiqueta> ReceitaEtiqueta { set; get; }
    }

    public class ReceitaEtiqueta {
        [Key]
        public int EtiquetaId { set; get; }
        [Key]
        public int ReceitaId { set; get; }
        [NotMapped]
        [JsonIgnore]
        public Receita Receita { set; get; }
        [NotMapped]
        [JsonIgnore]
        public Etiqueta Etiqueta { get; set; }
    }

    public class ReceitaIngrediente {
        public ReceitaIngrediente() { }
        public ReceitaIngrediente(int idReceita, int idIngrediente) {
            this.ReceitaId = idReceita;
            this.IngredienteId = idIngrediente;
        }
        [Key]
        public int ReceitaId { get; set; }
        [Key]
        public int IngredienteId { set; get; }
        [Required]
        public int Quantidade { set; get; }

        [NotMapped]
        [JsonIgnore]
        public virtual Ingrediente Ingrediente { set; get; }
        [NotMapped]
        [JsonIgnore]
        public virtual Receita Receita { set; get; }
    }

    public class ReceitaProcesso {
        public ReceitaProcesso() { }
        public ReceitaProcesso(int idReceita, int idProcesso) {
            this.ReceitaId = idReceita;
            this.ProcessoId = idProcesso;
        }
        [Key]
        public int ReceitaId { get; set; }
        [Key]
        public int ProcessoId { set; get; }

        [NotMapped]
        [JsonIgnore]
        public virtual Processo Processo { set; get; }
        [NotMapped]
        [JsonIgnore]
        public virtual Receita Receita { set; get; }
    }
}
