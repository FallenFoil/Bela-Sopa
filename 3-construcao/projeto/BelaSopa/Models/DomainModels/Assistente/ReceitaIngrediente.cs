using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class ReceitaIngrediente
    {
        public ReceitaIngrediente()
        {
        }

        public ReceitaIngrediente(int idReceita, int idIngrediente)
        {
            this.ReceitaId = idReceita;
            this.IngredienteId = idIngrediente;
        }

        [Key]
        public int ReceitaId { get; set; }

        [Key]
        public int IngredienteId { set; get; }

        [Required]
        public int Quantidade { set; get; }

        [NotMapped, JsonIgnore]
        public virtual Ingrediente Ingrediente { set; get; }

        [NotMapped, JsonIgnore]
        public virtual Receita Receita { set; get; }
    }
}
