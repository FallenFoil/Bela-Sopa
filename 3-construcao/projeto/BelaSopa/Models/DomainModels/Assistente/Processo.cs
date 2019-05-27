using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class Processo
    {

        public Processo()
        {

        }

        [Key]
        public int ProcessoId { set; get; }
        [Required]
        public int Tempo { set; get; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<ProcessoTarefa> ProcessoTarefa { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<ReceitaProcesso> ReceitaProcesso { get; set; }
    }

    public class ProcessoTarefa
    {
        public ProcessoTarefa() { }
        public ProcessoTarefa(int idProcesso, int idTarefa)
        {
            this.ProcessoId = idProcesso;
            this.TarefaId = idTarefa;
        }

        [Key]
        public int ProcessoId { set; get; }

        [NotMapped]
        [JsonIgnore]
        public virtual Processo Processo { get; set; }

        [Key]
        public int TarefaId { set; get; }

        [NotMapped]
        [JsonIgnore]
        public virtual Tarefa Tarefa { set; get; }
    }
}
