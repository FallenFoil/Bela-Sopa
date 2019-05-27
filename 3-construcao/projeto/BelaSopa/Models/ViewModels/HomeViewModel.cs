using BelaSopa.Models.DomainModels.Assistente;
using Microsoft.AspNetCore.Authorization;

namespace BelaSopa.Models.ViewModels
{
    [Authorize(Roles = "Cliente")]
    public class HomeViewModel
    {
        public Etiqueta[] Etiquetas { set; get; }

        public Receita[] Receitas { set; get; }
    }
}
