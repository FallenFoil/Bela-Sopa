using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class Utensilio
    {
        [Key]
        public int UtensilioId { get; set; }

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

        [NotMapped]
        public virtual ICollection<TarefaUtensilio> TarefaUtensilio { get; set; }

    }
}
