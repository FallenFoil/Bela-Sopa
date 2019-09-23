using BelaSopa.Models.DomainModels.Utilizadores;
using System;
using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class ClienteReceitaFinalizada
    {
        [Key]
        public int ClienteId { get; set; }

        [Key]
        public int ReceitaId { get; set; }

        [Key]
        public DateTime DataInicio { get; set; }

        [Required]
        public DateTime DataFim { get; set; }

        public Dificuldade? AvaliacaoDificuldade { set; get; }

        public virtual Cliente Cliente { get; set; }

        public virtual Receita Receita { get; set; }

        public double GetDuraçãoMinutos()
        {
            return DataFim.Subtract(DataInicio).Minutes;
        }
    }
}
