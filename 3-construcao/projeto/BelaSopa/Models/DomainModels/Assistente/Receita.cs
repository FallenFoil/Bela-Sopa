using BelaSopa.Shared;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class Receita
    {
        [Key]
        public int ReceitaId { get; set; }

        [Required, StringLength(100)]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public Dificuldade Dificuldade { get; set; }

        [Required]
        public int MinutosPreparacao { get; set; }

        [Required]
        public int NumeroDoses { get; set; }
        
        [NotMapped, JsonIgnore]
        public virtual ICollection<ReceitaEtiqueta> ReceitaEtiqueta { get; set; } = new List<ReceitaEtiqueta>();

        public virtual ICollection<UtilizacaoIngrediente> UtilizacoesIngredientes { get; set; } = new List<UtilizacaoIngrediente>();

        public virtual ICollection<ValorNutricional> ValoresNutricionais { get; set; } = new List<ValorNutricional>();

        public virtual ICollection<Processo> Processos { get; set; } = new List<Processo>();

        [NotMapped, JsonIgnore]
        public virtual ICollection<ClienteFavorito> ClienteFavorito { get; set; }

        [NotMapped, JsonIgnore]
        public virtual ICollection<ClienteFinalizado> ClienteFinalizado { get; set; }

        public virtual ICollection<RefeicaoEmentaSemanal> RefeicaoEmentaSemanal { get; set; } = new List<RefeicaoEmentaSemanal>();

        public (IList<Tecnica>, IList<Utensilio>) GetTecnicasUtensilios()
        {
            var textosTarefas =
                Processos
                .SelectMany(p => p.Tarefas)
                .SelectMany(t => t.Texto)
                .ToArray();

            return (
                textosTarefas
                    .Select(texto => texto.Tecnica)
                    .Where(t => t != null)
                    .DistinctBy(t => t.TecnicaId)
                    .OrderBy(t => t.Nome)
                    .ToArray(),
                textosTarefas
                    .Select(texto => texto.Utensilio)
                    .Where(u => u != null)
                    .DistinctBy(u => u.UtensilioId)
                    .OrderBy(u => u.Nome)
                    .ToArray()
                );
        }
    }
}
