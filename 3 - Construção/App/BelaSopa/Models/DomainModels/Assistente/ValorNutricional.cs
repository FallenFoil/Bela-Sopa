using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using YamlDotNet.Serialization;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class ValorNutricional
    {
        [Key]
        public int ValorNutricionalId { get; set; }

       // [Required, StringLength(50)]
        [YamlMember(Alias = "nome")]
        public string Nome { get; set; }

        [Required, StringLength(20)]
        [YamlMember(Alias = "dose")]
        public string Dose { get; set; }

        [YamlMember(Alias = "percentagem-do-vdr-adulto")]
        public int? PercentagemVdrAdulto { get; set; }

        [Required]
        public int ReceitaId { get; set; }

        public virtual Receita Receita { get; set; }
    }
}
