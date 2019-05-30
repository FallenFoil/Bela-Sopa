using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class Tecnica
    {
        [Key]
        public int TecnicaId { get; set; }

        [Required]
        [StringLength(20)]
        public string Nome { get; set; }

        [Required]
        [StringLength(200)]
        public string Descricao { get; set; }

        [Required]
        [StringLength(50)]
        public string ImagePath { get; set; }

        [Required]
        [StringLength(50)]
        public string Link { get; set; }

        //[NotMapped]
        //public virtual ICollection<TarefaTecnica> TarefaTecnica { get; set; }

    }
}
