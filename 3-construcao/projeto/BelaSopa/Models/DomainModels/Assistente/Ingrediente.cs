using BelaSopa.Shared;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using YamlDotNet.Serialization;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class Ingrediente
    {
        [Key]
        public int IngredienteId { get; set; }

        [Required, StringLength(100)]
        [YamlMember(Alias = "nome")]
        public string Nome { get; set; }

        [Required]
        [YamlMember(Alias = "descrição")]
        public string Descricao { get; set; }

        [Required]
        [YamlMember(Alias = "texto")]
        public string Texto { get; set; }

        [Required]
        [YamlMember(Alias = "imagem")]
        public byte[] Imagem { get; set; }

        public virtual ICollection<UtilizacaoIngrediente> Utilizacoes { get; set; } = new List<UtilizacaoIngrediente>();

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
