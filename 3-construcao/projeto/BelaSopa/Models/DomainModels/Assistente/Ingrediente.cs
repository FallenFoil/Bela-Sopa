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

        [NotMapped, JsonIgnore]
        public virtual ICollection<TarefaIngrediente> TarefaIngrediente { get; set; } = new List<TarefaIngrediente>();
        
        public virtual ICollection<ReceitaIngrediente> ReceitaIngrediente { get; set; } = new List<ReceitaIngrediente>();

        public string GetImagemBase64()
        {
            return Convert.ToBase64String(Imagem, Base64FormattingOptions.None);
        }
    }
}
