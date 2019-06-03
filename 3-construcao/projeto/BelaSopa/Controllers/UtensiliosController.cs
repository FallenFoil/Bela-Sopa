using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Assistente;
using BelaSopa.Models.ViewModels;
using BelaSopa.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            var receitas =
                context
                .TextoTarefa
                .Where(tt => tt.UtensilioId == id)
                .Include(tt => tt.Tarefa.Processo.Receita)
                .Select(tt => tt.Tarefa.Processo.Receita)
                .DistinctBy(r => r.ReceitaId)
                .OrderBy(r => r.Nome)
                .ToArray();

            // criar view model e devolver view

            return View(viewName: "DetalhesUtensilio", model: (Utensilio: utensilio, Receitas: receitas));
        }

        public IActionResult criarUtensilio(CriarUtensilioViewModel Utensilio)
        {
            if (Utensilio == null)
            {
                Utensilio = new CriarUtensilioViewModel();
            }

            return View(viewName: "CriarUtensilio");
        }

    }
}
