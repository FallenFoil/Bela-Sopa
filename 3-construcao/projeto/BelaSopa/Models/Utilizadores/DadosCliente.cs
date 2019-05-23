using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models.Utilizadores
{
    public class DadosCliente : Credenciais
    {
        [Display(Name = "Endereço de e-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "O endereço de e-mail é inválido.")]
        public string Email { set; get; }
    }
}
