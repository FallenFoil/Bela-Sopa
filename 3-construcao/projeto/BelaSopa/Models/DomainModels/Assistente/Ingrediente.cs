using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class Ingrediente
    {

        public Ingrediente()
        {
            this.TarefaIngrediente = new HashSet<TarefaIngrediente>();
        }

        [Key]
        public int IngredienteId { set; get; }

        [Required]
        [StringLength(20)]
        public string Nome { set; get; }

        [Required]
        [StringLength(200)]
        public string Descricao { set; get; }

        [Required]
        [StringLength(50)]
        public string ImagePath { set; get; }

        [Required]
        [StringLength(50)]
        public string Link { set; get; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<TarefaIngrediente> TarefaIngrediente { set; get; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<ReceitaIngrediente> ReceitaIngrediente { set; get; }
    }
}
