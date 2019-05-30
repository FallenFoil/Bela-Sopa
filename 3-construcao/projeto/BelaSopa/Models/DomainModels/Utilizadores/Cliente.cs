using BelaSopa.Models.DomainModels.Assistente;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelaSopa.Models.DomainModels.Utilizadores{
    public class Cliente : Utilizador{
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [NotMapped, JsonIgnore]
        public virtual ICollection<ClienteEmentaSemanal> ClienteEmentaSemanal { get; set; }
        [NotMapped, JsonIgnore]
        public virtual ICollection<ClienteFavorito> ClienteFavorito { set; get; }
        [NotMapped, JsonIgnore]
        public virtual ICollection<ClienteFinalizado> ClienteFinalizado { set; get; }
    }
}
