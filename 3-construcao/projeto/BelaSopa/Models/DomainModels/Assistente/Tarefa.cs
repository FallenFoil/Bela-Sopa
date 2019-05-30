using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class Tarefa
    {
        [Key]
        public int TarefaId { get; set; }

        [Required]
        public string Texto { get; set; }

        //[Required]
        //public int Tempo { get; set; }

        //[NotMapped, JsonIgnore]
        //public virtual ICollection<TarefaIngrediente> TarefaIngrediente { get; set; } = new List<TarefaIngrediente>();

        //[NotMapped, JsonIgnore]
        //public virtual ICollection<TarefaUtensilio> TarefaUtensilio { get; set; } = new List<TarefaUtensilio>();

        //[NotMapped, JsonIgnore]
        //public virtual ICollection<TarefaTecnica> TarefaTecnica { get; set; } = new List<TarefaTecnica>();

        [Required]
        public int ProcessoId { get; set; }

        public virtual Processo Processo { get; set; }
        
        public List<(string Controller, int? Id, string Texto)> GetPartesTexto()
        {
            var partes = new List<(string Controller, int? Id, string Texto)>();

            foreach (var parte in Texto.Split('|').Select(t => t.Trim()))
            {
                if (parte.StartsWith('$'))
                {
                    var split = parte.Substring(1).Split(',', 3);
                    partes.Add((split[0], int.Parse(split[1]), split[2].Trim()));
                }
                else
                {
                    partes.Add((null, null, parte));
                }
            }

            return partes;
        }
    }

    //public class TarefaIngrediente
    //{
    //    public TarefaIngrediente() { }
    //    public TarefaIngrediente(int idTarefa, int idIngrediente)
    //    {
    //        this.IngredienteId = idIngrediente;
    //        this.TarefaId = idTarefa;
    //    }

    //    [Key]
    //    public int TarefaId { get; set; }

    //    public virtual Tarefa Tarefa { get; set; }

    //    [Key]
    //    public int IngredienteId { get; set; }

    //    public virtual Ingrediente Ingrediente { get; set; }
    //}

    //public class TarefaUtensilio
    //{
    //    public TarefaUtensilio() { }
    //    public TarefaUtensilio(int idTarefa, int idUtensilio)
    //    {
    //        this.UtensilioId = idUtensilio;
    //        this.TarefaId = idTarefa;
    //    }

    //    [Key]
    //    public int TarefaId { get; set; }

    //    public virtual Tarefa Tarefa { get; set; }

    //    [Key]
    //    public int UtensilioId { get; set; }

    //    public virtual Utensilio Utensilio { get; set; }
    //}

    //public class TarefaTecnica
    //{
    //    public TarefaTecnica() { }
    //    public TarefaTecnica(int idTarefa, int idTecnica)
    //    {
    //        this.TecnicaId = idTecnica;
    //        this.TarefaId = idTarefa;
    //    }

    //    [Key]
    //    public int TarefaId { get; set; }

    //    public virtual Tarefa Tarefa { get; set; }

    //    [Key]
    //    public int TecnicaId { get; set; }

    //    public virtual Tecnica Tecnica { get; set; }
    //}
}
