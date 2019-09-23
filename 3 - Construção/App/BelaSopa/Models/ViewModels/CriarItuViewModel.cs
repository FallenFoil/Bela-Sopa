using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models.ViewModels
{
    public class CriarItuViewModel
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 carateres.")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; }

        [Display(Name = "Texto")]
        [Required(ErrorMessage = "O texto é obrigatório.")]
        public string Texto { get; set; }
    }
}
