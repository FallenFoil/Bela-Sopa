using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class ReceitaEtiqueta
    {
        [Key]
        public int ReceitaId { set; get; }

        [Key]
        public int EtiquetaId { set; get; }

        [NotMapped, JsonIgnore]
        public Receita Receita { set; get; }

        [NotMapped, JsonIgnore]
        public Etiqueta Etiqueta { get; set; }
    }
}
