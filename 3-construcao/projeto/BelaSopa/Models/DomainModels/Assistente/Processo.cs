using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class Processo
    {
        [Key]
        public int ProcessoId { get; set; }

        //[Required]
        //public int Tempo { get; set; }

        public virtual ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();

        [Required]
        public int ReceitaId { get; set; }

        public virtual Receita Receita { get; set; }
    }
}
