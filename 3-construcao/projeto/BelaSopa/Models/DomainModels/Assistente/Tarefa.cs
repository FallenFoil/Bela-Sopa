using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class Tarefa
    {
        public int TarefaId { get; set; }

        [Required, StringLength(200)]
        public string Descricao { get; set; }

        [Required]
        public int Tempo { get; set; }

        [NotMapped, JsonIgnore]
        public virtual ICollection<TarefaIngrediente> TarefaIngrediente { get; set; } = new List<TarefaIngrediente>();

        [NotMapped, JsonIgnore]
        public virtual ICollection<TarefaUtensilio> TarefaUtensilio { get; set; } = new List<TarefaUtensilio>();

        [NotMapped, JsonIgnore]
        public virtual ICollection<TarefaTecnica> TarefaTecnica { get; set; } = new List<TarefaTecnica>();

        [NotMapped, JsonIgnore]
        public virtual ICollection<ProcessoTarefa> ProcessoTarefa { get; set; } = new List<ProcessoTarefa>();
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
        public int TarefaId { get; set; }
        [NotMapped]
        public virtual Tarefa Tarefa { get; set; }

        [Key]
        public int IngredienteId { get; set; }
        [NotMapped]
        public virtual Ingrediente Ingrediente { get; set; }
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
        public int TarefaId { get; set; }
        [NotMapped]
        public virtual Tarefa Tarefa { get; set; }

        [Key]
        public int UtensilioId { get; set; }
        [NotMapped]
        public virtual Utensilio Utensilio { get; set; }
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
        public int TarefaId { get; set; }
        [NotMapped]
        public virtual Tarefa Tarefa { get; set; }

        [Key]
        public int TecnicaId { get; set; }
        [NotMapped]
        public virtual Tecnica Tecnica { get; set; }
    }
}
