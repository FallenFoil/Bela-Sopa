using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class NomeAlternativoUtensilio
    {
        [Key]
        public int NomeAlternativoUtensilioId { get; set; }

        [Required, StringLength(100)]
        public string Nome { get; set; }

        public int UtensilioId { get; set; }

        public virtual Utensilio Utensilio { get; set; }
    }
}
