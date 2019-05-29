using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class ReceitaProcesso
    {
        [Key]
        public int ReceitaId { get; set; }

        [Key]
        public int ProcessoId { set; get; }

        [NotMapped, JsonIgnore]
        public virtual Processo Processo { set; get; }

        [NotMapped, JsonIgnore]
        public virtual Receita Receita { set; get; }
    }
}
