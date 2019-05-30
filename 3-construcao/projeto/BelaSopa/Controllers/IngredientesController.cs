using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Assistente;
using BelaSopa.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BelaSopa.Controllers
{
    [Authorize]
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

            // obter ingredientes

            IQueryable<Ingrediente> ingredientes = context.Ingrediente;

            if (nome != null)
                ingredientes = ingredientes.Where(ingrediente => Util.FuzzyContains(ingrediente.Nome, nome));

            ingredientes = ingredientes.OrderBy(ingrediente => ingrediente.Nome);

            var viewModel = ingredientes.ToList();

            // criar view model e devolver view

            return View(viewName: "ListaIngredientes", model: viewModel);
        }

        [HttpGet]
        [Route("[controller]/[action]/{id}")]
        public IActionResult Detalhes([FromRoute] int id)
        {
            // obter ingrediente

            var ingrediente =
                context
                .Ingrediente
                .Include(i => i.Utilizacoes)
                .ThenInclude(ui => ui.Receita)
                .SingleOrDefault(i => i.IngredienteId == id);

            if (ingrediente == null)
                return NotFound();

            // criar view model e devolver view

            var viewModel = (
                Ingrediente: ingrediente,
                Seccoes: Util.FormatarTextoComSeccoes(ingrediente.Texto)
                );

            return View(viewName: "DetalhesIngrediente", model: viewModel);
        }
    }
}
