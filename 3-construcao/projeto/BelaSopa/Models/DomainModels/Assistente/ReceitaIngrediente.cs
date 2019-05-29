using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class ReceitaIngrediente
    {
        public ReceitaIngrediente() { }
        public ReceitaIngrediente(int idReceita, int idIngrediente) {
            this.ReceitaId = idReceita;
            this.IngredienteId = idIngrediente;
        }

        [Key]
        public int ReceitaIngredienteId { get; set; }

        [Required, StringLength(100)]
        public string Nome { get; set; }

        [Required, StringLength(50)]
        public string Quantidade { get; set; }

        public int ReceitaId { get; set; }

        public virtual Receita Receita { get; set; }

        public int? IngredienteId { get; set; }

        public virtual Ingrediente Ingrediente { get; set; }
    }
}
