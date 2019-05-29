using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class Tarefa
    {
        public int TarefaId { set; get; }

        [Required, StringLength(200)]
        public string Descricao { set; get; }

        [Required]
        public int Tempo { set; get; }

        [NotMapped, JsonIgnore]
        public virtual ICollection<TarefaIngrediente> TarefaIngrediente { set; get; } = new List<TarefaIngrediente>();

        [NotMapped, JsonIgnore]
        public virtual ICollection<TarefaUtensilio> TarefaUtensilio { set; get; } = new List<TarefaUtensilio>();

        [NotMapped, JsonIgnore]
        public virtual ICollection<TarefaTecnica> TarefaTecnica { set; get; } = new List<TarefaTecnica>();

        [NotMapped, JsonIgnore]
        public virtual ICollection<ProcessoTarefa> ProcessoTarefa { set; get; } = new List<ProcessoTarefa>();
    }

    public class TarefaIngrediente
    {
        public TarefaIngrediente() { }
        public TarefaIngrediente(int idTarefa, int idIngrediente)
        {
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
        public virtual Ingrediente Ingrediente { set; get; }
    }

    public class TarefaUtensilio
    {
        public TarefaUtensilio() { }
        public TarefaUtensilio(int idTarefa, int idUtensilio)
        {
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

    public class TarefaTecnica
    {
        public TarefaTecnica() { }
        public TarefaTecnica(int idTarefa, int idTecnica)
        {
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
