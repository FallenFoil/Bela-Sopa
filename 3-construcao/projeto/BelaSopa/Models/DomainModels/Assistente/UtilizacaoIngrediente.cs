using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using YamlDotNet.Serialization;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class UtilizacaoIngrediente
    {
        public UtilizacaoIngrediente() { }
        public UtilizacaoIngrediente(int idReceita, int idIngrediente) {
            this.ReceitaId = idReceita;
            this.IngredienteId = idIngrediente;
        }

        [Key]
        public int ReceitaIngredienteId { get; set; }

        [Required, StringLength(100)]
        [YamlMember(Alias = "nome")]
        public string Nome { get; set; }

        [Required, StringLength(50)]
        [YamlMember(Alias = "quantidade")]
        public string Quantidade { get; set; }

        public int ReceitaId { get; set; }

        public virtual Receita Receita { get; set; }

        public int? IngredienteId { get; set; }

        public virtual Ingrediente Ingrediente { get; set; }
    }
}
