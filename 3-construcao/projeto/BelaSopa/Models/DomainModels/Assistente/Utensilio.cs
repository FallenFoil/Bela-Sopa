using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class Utensilio
    {
        [Key]
        public int UtensilioId { get; set; }

        [Required, StringLength(100)]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public string Texto { get; set; }
        
        public virtual ICollection<NomeAlternativoUtensilio> NomesAlternativos { get; set; } = new List<NomeAlternativoUtensilio>();
    }
}
