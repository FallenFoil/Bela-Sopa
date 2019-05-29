using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class ReceitaIngrediente
    {
        [Key]
        public int ReceitaId { get; set; }

        [Key]
        public int IngredienteId { get; set; }

        [Required]
        public string Nome { get; set; }
        [Required]
        public string Quantidade { get; set; }

        [NotMapped, JsonIgnore]
        public virtual Ingrediente Ingrediente { get; set; }

        [NotMapped, JsonIgnore]
        public virtual Receita Receita { get; set; }
    }
}
