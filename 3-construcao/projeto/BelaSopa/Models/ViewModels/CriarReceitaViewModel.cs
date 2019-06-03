using BelaSopa.Models.DomainModels.Assistente;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BelaSopa.Models.ViewModels {
    public class CriarReceitaViewModel {
        public List<SelectListItem> Dificuldades { get; } = new List<SelectListItem> {
            new SelectListItem {
                Text = "Facil",
                Value = "Facil",
            },
            new SelectListItem {
                Text = "Medio",
                Value = "Medio",
            },
            new SelectListItem {
                Text = "Dificil",
                Value = "Dificil",
            }
        };

        public List<SelectListItem> Etiquetas { get; set; } = new List<SelectListItem>();

        [Display(Name = "Nome da Receita")]
        [Required(ErrorMessage = "O nome da receita é obrigatório.")]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "O nome da receita deve ter entre 4 e 32 carateres.")]
        public string NomeDeReceita { get; set; }

        [Display(Name = "Descrição")]
        [MaxLength(5000)]
        [Required(ErrorMessage = "A Descrição é obrigatória.")]
        public string Descricao { get; set; }

        [Display(Name = "Dificuldade")]
        [Required(ErrorMessage = "Dificuldade é obrigatória")]
        public Dificuldade Dificuldade { get; set; }
        public string DificuldadeStr { get; set; }

        [Display(Name = "Minutos de preparação")]
        [Required(ErrorMessage = "A duração da receita é obrigatória")]
        public int Minutos { get; set; }

        [Display(Name = "Doses")]
        [Required(ErrorMessage = "O número de doses é obrigatória")]
        public int Doses { get; set; }


        [Display(Name = "Valores Nutricionais")]
        public List<ValorNutricional> ValorNutricionais { get; set; } = new List<ValorNutricional>();


        [Display(Name = "Etiquetas")]
        public List<Etiqueta> ReceitaEtiqueta { get; set; } = new List<Etiqueta>();


        [Display(Name = "Ingredientes")]
        public List<UtilizacaoIngrediente> UtilizacoesIngredientes { get; set; } = new List<UtilizacaoIngrediente>();

        public List<Int32> Quantidades { set; get; } = new List<Int32>();

        /*
        [Display(Name = "Processos")]
        public List<TextoTarefa> Tarefas { get; set; } = new List<TextoTarefa>();
        */

        public List<List<string>> Processos { set; get; } = new List<List<string>>();
    }
}
