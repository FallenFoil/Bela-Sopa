using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class Tarefa
    {
        [Key]
        public int TarefaId { get; set; }

        [Required]
        public int Indice { get; set; }

        public virtual ICollection<TextoTarefa> Texto { get; set; } = new List<TextoTarefa>();

        [Required]
        public int ProcessoId { get; set; }

        public virtual Processo Processo { get; set; }
    }
}
