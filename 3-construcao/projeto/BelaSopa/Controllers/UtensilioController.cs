using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Assistente;
using BelaSopa.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BelaSopa.Controllers
{
    [Authorize]
    public class UtensiliosController : Controller
    {
        private readonly BelaSopaContext context;

        public UtensiliosController(BelaSopaContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] string nome)
        {
            ViewData["nome"] = nome;

            // obter utensílios

            IQueryable<Utensilio> utensilios = context.Utensilio;

            if (nome != null)
                utensilios = utensilios.Where(utensilio => Util.FuzzyContains(utensilio.Nome, nome));

            utensilios = utensilios.OrderBy(utensilio => utensilio.Nome);

            // criar view model e devolver view

            var viewModel = utensilios.ToList();

            return View(viewName: "ListaUtensilios", model: viewModel);
        }

        [HttpGet]
        [Route("[controller]/[action]/{id}")]
        public IActionResult Detalhes([FromRoute] int id)
        {
            // obter utensílio

            var utensilio =
                context
                .Utensilio
                .SingleOrDefault(i => i.UtensilioId == id);

            if (utensilio == null)
                return NotFound();

            // criar view model e devolver view

            var viewModel = (
                Utensilio: utensilio,
                Seccoes: Util.FormatarTextoComSeccoes(utensilio.Texto)
                );

            return View(viewName: "DetalhesUtensilio", model: viewModel);
        }
    }
}
