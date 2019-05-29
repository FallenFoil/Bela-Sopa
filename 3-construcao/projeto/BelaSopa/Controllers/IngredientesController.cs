using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Assistente;
using BelaSopa.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

            IQueryable<Ingrediente> ingredientes = context.Ingrediente;

            if (nome != null)
                ingredientes = ingredientes.Where(ingrediente => Util.FuzzyContains(ingrediente.Nome, nome));

            ingredientes = ingredientes.OrderBy(ingrediente => ingrediente.Nome);

            var viewModel = ingredientes.ToList();

            return View(viewName: "ListaIngredientes", model: viewModel);
        }

        [HttpGet]
        [Route("[controller]/[action]/{idIngrediente}")]
        public IActionResult Detalhes([FromRoute] int idIngrediente)
        {
            // obter ingrediente

            var ingrediente = context.Ingrediente.Find(idIngrediente);

            if (ingrediente == null)
                return NotFound();

            // separar texto em secções

            var seccoes = new List<(string Titulo, List<string> Paragrafos)>();

            foreach (string parte in ingrediente.Texto.Split('\n'))
            {
                var trimmed = parte.Trim();

                if (!trimmed.EndsWith('.') && !trimmed.EndsWith(':'))
                {
                    // título da secção

                    seccoes.Add((trimmed, new List<string>()));
                }
                else
                {
                    // parágrafo

                    if (seccoes.Count == 0)
                        seccoes.Add((null, new List<string>()));

                    seccoes.Last().Paragrafos.Add(trimmed);
                }
            }

            // criar view model e devolver view

            var viewModel = (
                ingrediente: ingrediente,
                seccoes: seccoes
                );

            return View(viewName: "DetalhesIngrediente", model: viewModel);
        }
    }
}
