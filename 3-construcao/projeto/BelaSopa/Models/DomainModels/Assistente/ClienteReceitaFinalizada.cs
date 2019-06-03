using BelaSopa.Models.DomainModels.Utilizadores;
using System;
using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class ClienteReceitaFinalizada
    {
        [Required]
        public int ClienteId { get; set; }

        [Required]
        public int ReceitaId { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        [Required]
        public DateTime DataFim { get; set; }

        public String Avaliacao { set; get; }

        public virtual Cliente Cliente { get; set; }

        public virtual Receita Receita { get; set; }

        public double GetDuraçãoMinutos() {
            return DataFim.Subtract(DataInicio).Minutes;
        }
    }
}
