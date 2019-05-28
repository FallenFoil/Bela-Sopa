using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YamlDotNet.Serialization;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class Ingrediente
    {
        public int IngredienteId { set; get; }

        [Required, StringLength(100)]
        [YamlMember(Alias = "nome")]
        public string Nome { set; get; }

        [Required]
        [YamlMember(Alias = "descrição")]
        public string Descricao { set; get; }

        [Required]
        [YamlMember(Alias = "texto")]
        public string Texto { set; get; }

        [Required]
        [YamlMember(Alias = "imagem")]
        public byte[] Imagem { set; get; }

        [NotMapped, JsonIgnore]
        public virtual ICollection<TarefaIngrediente> TarefaIngrediente { set; get; } = new List<TarefaIngrediente>();

        [NotMapped, JsonIgnore]
        public virtual ICollection<ReceitaIngrediente> ReceitaIngrediente { set; get; } = new List<ReceitaIngrediente>();

        public string GetImagemBase64()
        {
            return Convert.ToBase64String(Imagem, Base64FormattingOptions.None);
        }
    }
}
