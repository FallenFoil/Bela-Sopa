using BelaSopa.Models.BusinessModels.Utilizadores;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelaSopa.Models.Assistente
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
        public int ClienteId { set; get; }
        [Key]
        public int ReceitaId { set; get; }

        [NotMapped]
        [JsonIgnore]
        public virtual Cliente Cliente { set; get; }
        [NotMapped]
        [JsonIgnore]
        public virtual Receita Receita { set; get; }
    }


    public class ClienteEmentaSemanal : ClienteReceita
    {
        public ClienteEmentaSemanal(int idCliente, int idReceita) : base(idCliente, idReceita) { }
        public ClienteEmentaSemanal() { }
        [Required]
        public DateTime Horario { set; get; }
    }

    public class ClienteFavorito : ClienteReceita
    {
        public ClienteFavorito() { }
        public ClienteFavorito(int idCliente, int idReceita) : base(idCliente, idReceita) { }

    }
    public class ClienteFinalizado : ClienteReceita
    {
        public ClienteFinalizado() { }
        public ClienteFinalizado(int idCliente, int idReceita) : base(idCliente, idReceita) { }
    }

}
