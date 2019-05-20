using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BelaSopa.Models.Assistente{
    public class Tecnica{
        [Key]
        public int TecnicaId { set; get; }

        [Required]
        [StringLength(20)]
        public string Nome { set; get; }

        [Required]
        [StringLength(200)]
        public string Descricao { set; get; }

        [Required]
        [StringLength(50)]
        public string ImagePath { set; get; }

        [Required]
        [StringLength(50)]
        public string Link { set; get; }

        [NotMapped]
        public virtual ICollection<TarefaTecnica> TarefaTecnica { set; get; }

    }
}
