using BelaSopa.Models.DomainModels.Assistente;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BelaSopa.Models.ViewModels
{
    [Authorize(Roles = "Cliente")]
    public class HomeViewModel
    {
        [ViewData]
        public Etiqueta[] Etiquetas { set; get; }
        [ViewData]
        public Receita[] Receitas { set; get; }
    }
}
