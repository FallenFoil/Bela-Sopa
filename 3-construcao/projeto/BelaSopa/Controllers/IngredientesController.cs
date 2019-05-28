using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Assistente;
using BelaSopa.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BelaSopa.Controllers
{
    [Authorize(Roles = Autenticacao.ROLES_ADMINISTRADOR_OU_CLIENTE)]
    public class IngredientesController : Controller
    {
        private readonly BelaSopaContext context;

        public IngredientesController(BelaSopaContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] string nome)
        {
            ViewData["nome"] = nome;

            IQueryable<Ingrediente> ingredientes = context.Ingrediente;

            if (nome != null)
                ingredientes = ingredientes.Where(ingrediente => Util.FuzzyContains(ingrediente.Nome, nome));

            var viewModel = ingredientes.ToList();

            return View(viewName: "ListaIngredientes", model: viewModel);
        }
    }
}
