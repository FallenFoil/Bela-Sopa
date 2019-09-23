using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class Tecnica
    {
        [Key]
        public int TecnicaId { get; set; }

        [Required, StringLength(100)]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public string Texto { get; set; }

        public virtual ICollection<NomeAlternativoTecnica> NomesAlternativos { get; set; } = new List<NomeAlternativoTecnica>();
    }
}
