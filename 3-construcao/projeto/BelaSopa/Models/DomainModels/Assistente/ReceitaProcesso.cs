using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class ReceitaProcesso
    {
        public ReceitaProcesso() { }
        public ReceitaProcesso(int idReceita, int idProcesso) {
            this.ReceitaId = idReceita;
            this.ProcessoId = idProcesso;
        }

        [Key]
        public int ReceitaId { get; set; }

        [Key]
        public int ProcessoId { get; set; }

        [NotMapped, JsonIgnore]
        public virtual Processo Processo { get; set; }

        [NotMapped, JsonIgnore]
        public virtual Receita Receita { get; set; }
    }
}
