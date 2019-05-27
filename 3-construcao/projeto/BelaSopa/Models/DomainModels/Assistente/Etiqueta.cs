using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class Etiqueta
    {
        public int EtiquetaId { set; get; }

        [Required, StringLength(50)]
        public string Nome { set; get; }

        [NotMapped, JsonIgnore]
        public virtual ICollection<ReceitaEtiqueta> ReceitaEtiqueta { set; get; } = new List<ReceitaEtiqueta>();
    }
}
