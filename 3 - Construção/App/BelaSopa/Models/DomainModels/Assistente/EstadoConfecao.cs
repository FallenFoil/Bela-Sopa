using BelaSopa.Models.DomainModels.Utilizadores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BelaSopa.Models.DomainModels.Assistente {
    public class EstadoConfecao {
        [Required]
        public int NumProcesso { get; set; }
        [Required]
        public DateTime Inicio { get; set; }
        [Key]
        public int ClienteId { get; set; }
        [Required]
        public int ReceitaId { get; set; }

        public virtual Receita Receita { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}
