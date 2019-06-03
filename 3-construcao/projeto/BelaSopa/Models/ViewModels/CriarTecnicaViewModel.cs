using System.ComponentModel.DataAnnotations;

namespace BelaSopa.Models.ViewModels
{
    public class CriarTecnicaViewModel
    {
        [Display(Name = "Nome da tecnica")]
        [Required(ErrorMessage = "O nome da tecnica é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome da tecnica deve ter entre 2 a 100 carateres.")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; }

        [Display(Name = "Texto")]
        [Required(ErrorMessage = "O texto é obrigatória.")]
        public string Texto { get; set; }
    }
}
