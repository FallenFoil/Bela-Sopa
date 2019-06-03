using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models.ViewModels
{
    public class CriarUtensilioViewModel
    {
        [Display(Name = "Nome do utensílio")]
        [Required(ErrorMessage = "O nome do utensílio é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome do utensílio deve ter entre 2 e 100 carateres.")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; }

        [Display(Name = "Texto")]
        [Required(ErrorMessage = "O texto é obrigatória.")]
        public string Texto { get; set; }
    }
}
