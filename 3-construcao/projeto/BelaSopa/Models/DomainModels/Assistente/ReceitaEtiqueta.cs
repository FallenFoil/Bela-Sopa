using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class ReceitaEtiqueta
    {
        [Key]
        public int ReceitaId { get; set; }

        [Key]
        public int EtiquetaId { get; set; }

        [NotMapped, JsonIgnore]
        public Receita Receita { get; set; }

        [NotMapped, JsonIgnore]
        public Etiqueta Etiqueta { get; set; }
    }
}
