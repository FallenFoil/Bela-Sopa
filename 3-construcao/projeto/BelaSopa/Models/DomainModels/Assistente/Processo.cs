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
        public int ProcessoId { get; set; }
        [Required]
        public int Tempo { get; set; }
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
        public int ProcessoId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Processo Processo { get; set; }

        [Key]
        public int TarefaId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Tarefa Tarefa { get; set; }
    }
}
