using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class TextoTarefa
    {
        [Key]
        public int TextoTarefaId { get; set; }

        [Required]
        public string Texto { get; set; }

        public int? IngredienteId { get; set; }

        public virtual Ingrediente Ingrediente { get; set; }

        public int? TecnicaId { get; set; }

        public virtual Tecnica Tecnica { get; set; }

        public int? UtensilioId { get; set; }

        public virtual Utensilio Utensilio { get; set; }

        public int TarefaId { get; set; }

        public virtual Tarefa Tarefa { get; set; }
    }
}
