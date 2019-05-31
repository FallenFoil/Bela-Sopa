using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class NomeAlternativoIngrediente
    {
        [Key]
        public int NomeAlternativoIngredienteId { get; set; }

        [Required, StringLength(100)]
        public string Nome { get; set; }

        public int IngredienteId { get; set; }

        public virtual Ingrediente Ingrediente { get; set; }
    }
}
