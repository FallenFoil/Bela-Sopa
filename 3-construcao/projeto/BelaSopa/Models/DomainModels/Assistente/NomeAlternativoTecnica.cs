using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class NomeAlternativoTecnica
    {
        [Key]
        public int NomeAlternativoTecnicaId { get; set; }

        [Required, StringLength(100)]
        public string Valor { get; set; }

        public int TecnicaId { get; set; }

        public virtual Tecnica Tecnica { get; set; }
    }
}
