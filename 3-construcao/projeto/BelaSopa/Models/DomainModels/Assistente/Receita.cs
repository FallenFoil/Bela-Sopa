using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class Receita
    {
        [Key]
        public int ReceitaId { get; set; }

        [Required, StringLength(100)]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public Dificuldade Dificuldade { get; set; }

        [Required]
        public int MinutosPreparacao { get; set; }

        [Required]
        public int NumeroDoses { get; set; }

        [Required]
        public byte[] Imagem { get; set; }

        [NotMapped, JsonIgnore]
        public virtual ICollection<ReceitaEtiqueta> ReceitaEtiqueta { get; set; } = new List<ReceitaEtiqueta>();

        public virtual ICollection<ReceitaIngrediente> Ingredientes { get; set; } = new List<ReceitaIngrediente>();

        public virtual ICollection<ValorNutricional> ValoresNutricionais { get; set; } = new List<ValorNutricional>();

        public virtual ICollection<Processo> Processos { get; set; } = new List<Processo>();

        [NotMapped, JsonIgnore]
        public virtual ICollection<ClienteFavorito> ClienteFavorito { get; set; }

        [NotMapped, JsonIgnore]
        public virtual ICollection<ClienteFinalizado> ClienteFinalizado { get; set; }

        [NotMapped, JsonIgnore]
        public virtual ICollection<ClienteEmentaSemanal> ClienteEmentaSemanal { get; set; }

        //[NotMapped, JsonIgnore]
        //public virtual ICollection<QuantidadeIngrediente> QuantidadeIngredientes { get; set; }

        //[NotMapped, JsonIgnore]
        //public virtual ICollection<ValorNutricional> ValorNutricionais { get; set; }

        //[StringLength(50)]
        //public string Video { get; set; }

        //[Required]
        //[StringLength(50)]
        //public string Link { get; set; }
        
        public bool HasEtiqueta(int etiquetaId)
        {
            foreach (ReceitaEtiqueta re in this.ReceitaEtiqueta)
                if (re.EtiquetaId == etiquetaId)
                    return true;

            return false;
        }
    }
}
