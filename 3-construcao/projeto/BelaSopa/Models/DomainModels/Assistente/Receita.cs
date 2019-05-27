using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class Receita
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public Dificuldade Dificuldade { get; set; }

        [Required]
        public int MinutosPreparacao { get; set; }

        [Required]
        public int NumDoses { get; set; }

        [Required]
        public byte[] Imagem { set; get; }

        //[NotMapped, JsonIgnore]
        //public virtual ICollection<QuantidadeIngrediente> QuantidadeIngredientes { get; set; }

        //[NotMapped, JsonIgnore]
        //public virtual ICollection<ValorNutricional> ValorNutricionais { get; set; }

        //[StringLength(50)]
        //public string Video { get; set; }

        //[Required]
        //[StringLength(50)]
        //public string Link { get; set; }

        [NotMapped, JsonIgnore]
        public virtual ICollection<ReceitaEtiqueta> ReceitaEtiqueta { get; set; } = new List<ReceitaEtiqueta>();

        [NotMapped, JsonIgnore]
        public virtual ICollection<ReceitaProcesso> ReceitaProcesso { get; set; }

        [NotMapped, JsonIgnore]
        public virtual ICollection<ReceitaIngrediente> ReceitaIngrediente { get; set; }

        [NotMapped, JsonIgnore]
        public virtual ICollection<ClienteFavorito> ClienteFavorito { get; set; }

        [NotMapped, JsonIgnore]
        public virtual ICollection<ClienteFinalizado> ClienteFinalizado { get; set; }

        [NotMapped, JsonIgnore]
        public virtual ICollection<ClienteEmentaSemanal> ClienteEmentaSemanal { get; set; }

        public string GetImagemBase64()
        {
            return Convert.ToBase64String(Imagem, Base64FormattingOptions.None);
        }
    }

    public class ReceitaEtiqueta
    {
        [Key]
        public int EtiquetaId { set; get; }
        [Key]
        public int ReceitaId { set; get; }
        [NotMapped]
        [JsonIgnore]
        public Receita Receita { set; get; }
        [NotMapped]
        [JsonIgnore]
        public Etiqueta Etiqueta { get; set; }
    }

    public class ReceitaIngrediente
    {
        public ReceitaIngrediente() { }
        public ReceitaIngrediente(int idReceita, int idIngrediente)
        {
            this.ReceitaId = idReceita;
            this.IngredienteId = idIngrediente;
        }
        [Key]
        public int ReceitaId { get; set; }
        [Key]
        public int IngredienteId { set; get; }
        [Required]
        public int Quantidade { set; get; }

        [NotMapped]
        [JsonIgnore]
        public virtual Ingrediente Ingrediente { set; get; }
        [NotMapped]
        [JsonIgnore]
        public virtual Receita Receita { set; get; }
    }

    public class ReceitaProcesso
    {
        public ReceitaProcesso() { }
        public ReceitaProcesso(int idReceita, int idProcesso)
        {
            this.ReceitaId = idReceita;
            this.ProcessoId = idProcesso;
        }
        [Key]
        public int ReceitaId { get; set; }
        [Key]
        public int ProcessoId { set; get; }

        [NotMapped]
        [JsonIgnore]
        public virtual Processo Processo { set; get; }
        [NotMapped]
        [JsonIgnore]
        public virtual Receita Receita { set; get; }
    }
}
