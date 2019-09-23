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
    public class TecnicasController : Controller
    {
        private readonly BelaSopaContext context;

        public TecnicasController(BelaSopaContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] string nome)
        {
            ViewData["nome"] = nome;

            // obter técnicas

            IQueryable<Tecnica> tecnicas = context.Tecnica;

            if (nome != null)
                tecnicas = tecnicas.Where(tecnica => Util.FuzzyContains(tecnica.Nome, nome));

            tecnicas = tecnicas.OrderBy(tecnica => tecnica.Nome);

            // criar view model e devolver view

            var viewModel = tecnicas.ToList();

            return View(viewName: "ListaTecnicas", model: viewModel);
        }

        [HttpGet]
        [Route("[controller]/[action]/{id}")]
        public IActionResult Detalhes([FromRoute] int id)
        {
            // obter técnica

            var tecnica =
                context
                .Tecnica
                .SingleOrDefault(i => i.TecnicaId == id);

            if (tecnica == null)
                return NotFound();

            var receitas =
                context
                .TextoTarefa
                .Where(tt => tt.TecnicaId == id)
                .Include(tt => tt.Tarefa.Processo.Receita)
                .Select(tt => tt.Tarefa.Processo.Receita)
                .DistinctBy(r => r.ReceitaId)
                .OrderBy(r => r.Nome)
                .ToArray();

            // criar view model e devolver view

            return View(viewName: "DetalhesTecnica", model: (Tecnica: tecnica, Receitas: receitas));
        }
    }
}
