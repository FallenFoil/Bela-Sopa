using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BelaSopa.Models.Assistente {
    public class Etiqueta {
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
