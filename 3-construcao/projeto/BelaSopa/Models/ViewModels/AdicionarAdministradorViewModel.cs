using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models.ViewModels
{
    public class AdicionarAdministradorViewModel
    {
        [Display(Name = "Nome de utilizador")]
        [Required(ErrorMessage = "O nome de utilizador é obrigatório.")]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "O nome de utilizador deve ter entre 4 e 32 carateres.")]
        public string NomeDeUtilizador { get; set; }

        [Display(Name = "Palavra-passe")]
        [Required(ErrorMessage = "A palavra-passe é obrigatória.")]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "A palavra-passe deve ter entre 4 e 32 carateres.")]
        [DataType(DataType.Password, ErrorMessage = "A palavra-passe é inválida.")]
        public string PalavraPasse { get; set; }

        [Display(Name = "Confirmação da palavra-passe")]
        [Required(ErrorMessage = "A confirmação da palavra-passe é obrigatória.")]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "A palavra-passe deve ter entre 4 e 32 carateres.")]
        [DataType(DataType.Password, ErrorMessage = "A palavra-passe é inválida.")]
        [Compare("PalavraPasse", ErrorMessage = "As palavras-passe não correspondem.")]
        public string ConfirmacaoPalavraPasse { get; set; }
    }
}
