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


    public class ClienteEmentaSemanal : ClienteReceita
    {
        public ClienteEmentaSemanal(int idCliente, int idReceita) : base(idCliente, idReceita) { }
        public ClienteEmentaSemanal() { }
        [Required]
        public TimeSpan Horario { get; set; }
    }

    public class ClienteFavorito : ClienteReceita
    {
        public ClienteFavorito() { }
        public ClienteFavorito(int idCliente, int idReceita) : base(idCliente, idReceita) { }

    }
    public class ClienteFinalizado : ClienteReceita {
        public ClienteFinalizado() { }
        public ClienteFinalizado(int idCliente, int idReceita) : base(idCliente, idReceita) {
            this.Data = DateTime.Now;
        }
        [Required]
        public DateTime Data {get; set;}
}

}
