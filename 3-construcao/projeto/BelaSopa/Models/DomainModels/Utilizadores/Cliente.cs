using BelaSopa.Models.DomainModels.Assistente;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelaSopa.Models.DomainModels.Utilizadores
{
    public class Cliente : Utilizador
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public virtual ICollection<RefeicaoEmentaSemanal> RefeicaoEmentaSemanal { get; set; } = new List<RefeicaoEmentaSemanal>();

        [NotMapped, JsonIgnore]
        public virtual ICollection<ClienteFavorito> ClienteFavorito { set; get; } = new List<ClienteFavorito>();
        [NotMapped, JsonIgnore]
        public virtual ICollection<ClienteFinalizado> ClienteFinalizado { set; get; } = new List<ClienteFinalizado>();

        [NotMapped, JsonIgnore]
        public virtual ICollection<ClienteExcluiIngrediente> ClienteExcluiIngrediente { set; get; } = new List<ClienteExcluiIngrediente>();
    }
}
