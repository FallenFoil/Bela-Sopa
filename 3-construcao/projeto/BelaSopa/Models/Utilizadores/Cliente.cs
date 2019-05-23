using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models.Utilizadores
{
    public class Cliente : Utilizador
    {
        [DataType(DataType.EmailAddress)]
        public string Email { set; get; }
    }
}
