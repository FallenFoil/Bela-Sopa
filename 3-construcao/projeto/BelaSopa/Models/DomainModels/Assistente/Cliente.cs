using BelaSopa.Models.DomainModels.Utilizadores;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelaSopa.Models.DomainModels.Assistente
{
    public class ClienteReceita
    {
        public ClienteReceita() { }
        public ClienteReceita(int idCliente, int idReceita)
        {
            this.ClienteId = idCliente;
            this.ReceitaId = idReceita;
        }
        [Key]
        public int ClienteId { get; set; }
        [Key]
        public int ReceitaId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Cliente Cliente { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Receita Receita { get; set; }
    }

    public class ClienteFavorito : ClienteReceita
    {
        public ClienteFavorito() { }
        public ClienteFavorito(int idCliente, int idReceita) : base(idCliente, idReceita) { }

    }
    public class ClienteFinalizado : ClienteReceita
    {
        public ClienteFinalizado() { }
        public ClienteFinalizado(int idCliente, int idReceita) : base(idCliente, idReceita)
        {
            this.Data = DateTime.Now;
        }
        [Required]
        public DateTime Data { get; set; }
    }

    public class ClienteExcluiIngrediente
    {
        [Key]
        public int ClienteId { set; get; }

        [Key]
        public int IngredienteId { set; get; }

        public virtual Cliente Cliente { get; set; }

        public virtual Ingrediente Ingrediente { get; set; }
    }
}
