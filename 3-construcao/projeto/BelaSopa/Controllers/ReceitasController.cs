using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Assistente;
using BelaSopa.Models.ViewModels;
using BelaSopa.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
                receitas = receitas.Where(receita => Util.FuzzyContains(receita.Nome, nome));

            if (etiqueta != null)
                receitas = receitas.Where(receita => receita.ReceitaEtiqueta.Any(e => e.EtiquetaId == etiqueta));

            if (dificuldade != null)
                receitas = receitas.Where(receita => receita.Dificuldade == dificuldade);

            receitas = receitas.OrderBy(receita => receita.Nome);

            var viewModel = (
                Etiquetas: context.Etiqueta.OrderBy(e => e.Nome).ToList(),
                Receitas: receitas.ToList()
                );

            return View(viewName: "ListaReceitas", model: viewModel);
        }

        [HttpGet]
        [Route("[controller]/[action]/{id}")]
        public IActionResult Detalhes([FromRoute] int id)
        {
            // obter receita

            var receita =
                context
                .Receita
                .Include(r => r.ReceitaEtiqueta)
                    .ThenInclude(re => re.Etiqueta)
                .Include(r => r.UtilizacoesIngredientes)
                    .ThenInclude(ui => ui.Ingrediente)
                .Include(r => r.ValoresNutricionais)
                .Include(r => r.Processos)
                    .ThenInclude(p => p.Tarefas)
                    .ThenInclude(t => t.Texto)
                    .ThenInclude(t => t.Ingrediente)
                .Include(r => r.Processos)
                    .ThenInclude(p => p.Tarefas)
                    .ThenInclude(t => t.Texto)
                    .ThenInclude(t => t.Tecnica)
                .Include(r => r.Processos)
                    .ThenInclude(p => p.Tarefas)
                    .ThenInclude(t => t.Texto)
                    .ThenInclude(t => t.Utensilio)
                .SingleOrDefault(i => i.ReceitaId == id);

            if (receita == null)
                return NotFound();

            var (tecnicas, utensilios) = receita.GetTecnicasUtensilios();

            var viewModel = (
                Receita: receita,
                Tecnicas: tecnicas,
                Utensilios: utensilios,
                Favorita: IsFavorito(receita.ReceitaId)
                );

            // devolver view

            return View(viewName: "DetalhesReceita", model: viewModel);
        }

       


        public IActionResult ToggleFavorito(int? id)
        {
            if (id.HasValue)
            {
                bool Favorita = IsFavorito(id.Value);
                int idCliente = Autenticacao.GetUtilizadorAutenticado(this, context).UtilizadorId;
                ClienteFavorito cf = new ClienteFavorito(idCliente, id.Value);
                if (Favorita)
                {
                    context.ClienteFavorito.Remove(cf);
                }
                else
                {
                    context.ClienteFavorito.Add(cf);
                }
                context.SaveChanges();
                return Detalhes(id.Value);
            }
            else { return NotFound(); }
        }

        public bool IsFavorito(int idReceita)
        {
            bool Favorita = context.ClienteFavorito.Any(f => f.ReceitaId == idReceita &&
                                                            f.ClienteId == Autenticacao.GetUtilizadorAutenticado(this, context).UtilizadorId);
            return Favorita;
        }

        public IActionResult AdicionarNovaReceita([Bind]AdicionarNovaReceitaViewModel Receita) {
            if(Receita == null) {
                Receita = new AdicionarNovaReceitaViewModel();
            }

            if (ModelState.IsValid) {
                return Ok();
            }

            List<Etiqueta> ets = context.Etiqueta.ToList<Etiqueta>();
            foreach(Etiqueta et in ets) {
                Receita.Etiquetas.Add(new SelectListItem { Value = et.EtiquetaId.ToString(), Text = et.Nome });
            }
            
            return View(viewName: "AdicionarNovaReceitaView", model: Receita);
        }

        public IActionResult AdicionarValorNutricional(AdicionarNovaReceitaViewModel Receita) {
            if (Receita == null) return NotFound();//AdicionarNovaReceita(Receita);
            Receita.ValorNutricionais.Add(new ValorNutricional());
            
            
            return AdicionarNovaReceita(Receita);
        }

        public IActionResult AdicionarEtiqueta(AdicionarNovaReceitaViewModel Receita) {
            if (Receita == null) return NotFound();//AdicionarNovaReceita(Receita);
            Receita.ReceitaEtiqueta.Add(new Etiqueta());
            return AdicionarNovaReceita(Receita);
        }
    }
}
