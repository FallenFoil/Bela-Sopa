using BelaSopa.Models.DomainModels.Assistente;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace BelaSopa.Models.ViewModels
{
    public class CriarUtensilioViewModel
    {

        [Display(Name = "Nome do Utensilio")]
        [Required(ErrorMessage = "O nome do utensilio é obrigatório.")]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "O nome do utensilio deve ter entre 4 e 32 carateres.")]
        public string NomeDoUtensilio { get; set; }

        [Display(Name = "Descrição")]
        [MaxLength(5000)]
        [Required(ErrorMessage = "A Descrição é obrigatória.")]
        public string DescricaoDoUtensilio { get; set; }


        [Display(Name = "Texto")]
        [MaxLength(5000)]
        [Required(ErrorMessage = "O texto é obrigatória.")]
        public string TextoDoUtensilio { get; set; }


    }
}
