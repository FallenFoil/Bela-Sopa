using System.ComponentModel.DataAnnotations;
using YamlDotNet.Serialization;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class Tecnica
    {
        [Key]
        public int TecnicaId { get; set; }

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

    }
}
