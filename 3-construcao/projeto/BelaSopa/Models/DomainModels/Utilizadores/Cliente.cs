using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models.DomainModels.Utilizadores
{
    public class Cliente : Utilizador
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
