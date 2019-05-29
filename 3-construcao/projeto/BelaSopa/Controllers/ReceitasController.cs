using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Assistente;
using BelaSopa.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BelaSopa.Controllers
{
    [Authorize]
    public class ReceitasController : Controller
    {
        private readonly BelaSopaContext context;

        public ReceitasController(BelaSopaContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index(
            [FromQuery] string nome,
            [FromQuery] int? etiqueta,
            [FromQuery] Dificuldade? dificuldade
            )
        {
            ViewData["nome"] = nome;
            ViewData["etiqueta"] = etiqueta;
            ViewData["dificuldade"] = dificuldade;

            IQueryable<Receita> receitas = context.Receita;

            if (nome != null)
                receitas = receitas.Where(receita => Util.FairlyFuzzyContains(receita.Nome, nome));

            if (etiqueta != null)
                receitas = receitas.Where(receita => receita.ReceitaEtiqueta.Any(e => e.EtiquetaId == etiqueta));

            if (dificuldade != null)
                receitas = receitas.Where(receita => receita.Dificuldade == dificuldade);

            receitas = receitas.OrderBy(receita => receita.Nome);

            var viewModel = (
                Etiquetas: context.Etiqueta.ToList(),
                Receitas: receitas.ToList()
                );

            return View(viewName: "ListaReceitas", model: viewModel);
        }

        [HttpGet]
        [Route("[controller]/[action]/{idReceita}")]
        public IActionResult Detalhes([FromRoute] int idReceita)
        {
            // obter receita

            var receita = context.Receita.Find(idReceita);

            if (receita == null)
                return NotFound();

            // devolver view

            return View(viewName: "DetalhesReceita", model: receita);
        }
    }
}
