using BelaSopa.Shared;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class Ingrediente
    {
        [Key]
        public int IngredienteId { get; set; }

        [Required, StringLength(100)]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public string Texto { get; set; }
        
        public virtual ICollection<NomeAlternativoIngrediente> NomesAlternativos { get; set; } = new List<NomeAlternativoIngrediente>();

        public virtual ICollection<UtilizacaoIngrediente> Utilizacoes { get; set; } = new List<UtilizacaoIngrediente>();

        public virtual ICollection<ClienteExcluiIngrediente> ClienteExcluiIngrediente { get; set; } = new List<ClienteExcluiIngrediente>();

        public List<Receita> GetReceitasUtilizacoes()
        {
            return
                Utilizacoes
                .Select(ui => ui.Receita)
                .DistinctBy(r => r.ReceitaId)
                .OrderBy(r => r.Nome)
                .ToList();
        }
    }
}
