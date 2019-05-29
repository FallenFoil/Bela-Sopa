using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models.ViewModels
{
    public class AlterarPalavraPasseViewModel
    {
        [Display(Name = "Palavra-passe atual")]
        [Required(ErrorMessage = "A palavra-passe atual é obrigatória.")]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "A palavra-passe deve ter entre 4 e 32 carateres.")]
        [DataType(DataType.Password, ErrorMessage = "A palavra-passe é inválida.")]
        public string PalavraPasseAtual { get; set; }

        [Display(Name = "Nova palavra-passe")]
        [Required(ErrorMessage = "A nova palavra-passe é obrigatória.")]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "A palavra-passe deve ter entre 4 e 32 carateres.")]
        [DataType(DataType.Password, ErrorMessage = "A palavra-passe é inválida.")]
        public string NovaPalavraPasse { get; set; }

        [Display(Name = "Confirmação da nova palavra-passe")]
        [Required(ErrorMessage = "A confirmação da nova palavra-passe é obrigatória.")]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "A palavra-passe deve ter entre 4 e 32 carateres.")]
        [DataType(DataType.Password, ErrorMessage = "A palavra-passe é inválida.")]
        [Compare("NovaPalavraPasse", ErrorMessage = "As palavras-passe não correspondem.")]
        public string ConfirmacaoNovaPalavraPasse { get; set; }
    }
}
