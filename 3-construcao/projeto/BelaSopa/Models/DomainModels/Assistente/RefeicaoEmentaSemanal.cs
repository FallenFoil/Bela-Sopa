using BelaSopa.Models.DomainModels.Utilizadores;
using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class RefeicaoEmentaSemanal
    {
        [Key]
        public int ClienteId { get; set; }

        [Key]
        public DiaDaSemana DiaDaSemana { get; set; }

        [Key]
        public RefeicaoDoDia RefeicaoDoDia { get; set; }

        [Required]
        public int ReceitaId { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual Receita Receita { get; set; }
    }
}
