using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BelaSopa.Models.Assistente{
    public class Tarefa{

        public Tarefa(){
            this.TarefaIngrediente = new HashSet<TarefaIngrediente>();
            this.TarefaUtensilio = new HashSet<TarefaUtensilio>();
            this.TarefaTecnica = new HashSet<TarefaTecnica>();
            this.ProcessoTarefa = new HashSet<ProcessoTarefa>();
        }

        [Key]
        public int TarefaId { set; get; }

        [Required]
        [StringLength(200)]
        public string Descricao { set; get; }

        [Required]
        public int Tempo { set; get; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<TarefaIngrediente> TarefaIngrediente { set; get; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<TarefaUtensilio> TarefaUtensilio { set; get; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<TarefaTecnica> TarefaTecnica { set; get; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<ProcessoTarefa> ProcessoTarefa { set; get; }
    }

    public class TarefaIngrediente {
        public TarefaIngrediente() { }
        public TarefaIngrediente(int idTarefa, int idIngrediente) {
            this.IngredienteId = idIngrediente;
            this.TarefaId = idTarefa;
        }

        [Key]
        public int TarefaId { set; get; }
        [NotMapped]
        public virtual Tarefa Tarefa { set; get; }

        [Key]
        public int IngredienteId { set; get; }
        [NotMapped]
        public virtual Ingrediente Ingrediente {set; get;}
    }

    public class TarefaUtensilio {
        public TarefaUtensilio() { }
        public TarefaUtensilio(int idTarefa, int idUtensilio) {
            this.UtensilioId = idUtensilio;
            this.TarefaId = idTarefa;
        }

        [Key]
        public int TarefaId { set; get; }
        [NotMapped]
        public virtual Tarefa Tarefa { set; get; }

        [Key]
        public int UtensilioId { set; get; }
        [NotMapped]
        public virtual Utensilio Utensilio { set; get; }
    }

    public class TarefaTecnica {
        public TarefaTecnica() { }
        public TarefaTecnica(int idTarefa, int idTecnica) {
            this.TecnicaId = idTecnica;
            this.TarefaId = idTarefa;
        }

        [Key]
        public int TarefaId { set; get; }
        [NotMapped]
        public virtual Tarefa Tarefa { set; get; }

        [Key]
        public int TecnicaId { set; get; }
        [NotMapped]
        public virtual Tecnica Tecnica { set; get; }
    }
}
