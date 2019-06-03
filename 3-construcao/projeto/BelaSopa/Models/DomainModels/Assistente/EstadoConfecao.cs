using BelaSopa.Models.DomainModels.Utilizadores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BelaSopa.Models.DomainModels.Assistente {
    public class EstadoConfecao {
        [Required]
        public int NumProcesso { set; get; }
        [Required]
        public DateTime Inicio { set; get; }
        [Key]
        public int ClienteId { set; get; }
        [Required]
        public int ReceitaId { set; get; }

        public virtual Receita Receita { set; get; }

        public virtual Cliente Cliente { set; get; } 
    }
}
