using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class Etiqueta
    {
        public int EtiquetaId { get; set; }

        [Required, StringLength(50)]
        public string Nome { get; set; }

        [NotMapped, JsonIgnore]
        public virtual ICollection<ReceitaEtiqueta> ReceitaEtiqueta { get; set; } = new List<ReceitaEtiqueta>();
    }
}
