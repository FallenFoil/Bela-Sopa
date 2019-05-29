using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models.ViewModels
{
    public class AlterarEmailViewModel
    {
        [Display(Name = "Endereço de e-mail (opcional)")]
        [DataType(DataType.EmailAddress, ErrorMessage = "O endereço de e-mail é inválido.")]
        public string NovoEmail { set; get; }
    }
}
