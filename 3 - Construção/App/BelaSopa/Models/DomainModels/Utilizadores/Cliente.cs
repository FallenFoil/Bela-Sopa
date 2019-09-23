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
        public virtual ICollection<ClienteReceitaFavorita> ClienteFavorito { get; set; } = new List<ClienteReceitaFavorita>();

        [NotMapped, JsonIgnore]
        public virtual ICollection<ClienteReceitaFinalizada> ClienteFinalizado { get; set; } = new List<ClienteReceitaFinalizada>();

        [NotMapped, JsonIgnore]
        public virtual ICollection<ClienteExcluiIngrediente> ClienteExcluiIngrediente { get; set; } = new List<ClienteExcluiIngrediente>();

        public EstadoConfecao EstadoConfecao { get; set; }
    }
}
