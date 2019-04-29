using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models.Assistente {
    public class Processo{
        [Key]
        public int Id;

        [Required]
        public int Tempo;

        public virtual List<Tarefa> Tarefas { get; set; }
    }
}
