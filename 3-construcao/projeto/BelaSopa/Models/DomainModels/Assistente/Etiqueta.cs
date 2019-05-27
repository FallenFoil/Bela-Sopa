using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class Etiqueta
    {
        [Key]
        public int EtiquetaId { set; get; }
        [Required]
        [StringLength(20)]
        public string Nome { set; get; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<ReceitaEtiqueta> ReceitaEtiqueta { set; get; }
    }
}
