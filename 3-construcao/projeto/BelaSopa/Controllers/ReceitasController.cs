using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Assistente;
using BelaSopa.Models.DomainModels.Utilizadores;
using BelaSopa.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

            IQueryable<Receita> receitas =
                context
                .Receita
                .Include(r => r.ReceitaEtiqueta)
                .Include(r => r.UtilizacoesIngredientes);

            if (Autenticacao.GetUtilizadorAutenticado(this, context) is Cliente cliente)
            {
                var idsIngredientesExcluidos =
                    context
                    .ClienteExcluiIngrediente
                    .Where(cei => cei.ClienteId == cliente.UtilizadorId)
                    .Select(cei => cei.IngredienteId)
                    .ToHashSet();

                receitas = receitas.Where(
                    receita => !receita.UtilizacoesIngredientes.Any(
                        ui => ui.IngredienteId.HasValue && idsIngredientesExcluidos.Contains(ui.IngredienteId.Value)
                        )
                    );
            }

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
                Favorita: ReceitaIsFavorita(id)
                );

            // devolver view

            return View(viewName: "DetalhesReceita", model: viewModel);
        }

        [HttpGet]
        [Route("[controller]/[action]/{id}")]
        public IActionResult Favorita([FromRoute] int id, [FromQuery] bool favorita)
        {
            if (Autenticacao.GetUtilizadorAutenticado(this, context) is Cliente cliente)
            {
                var favorito = context.ClienteReceitaFavorita.Find(cliente.UtilizadorId, id);

                if (favorito == null)
                {
                    context.ClienteReceitaFavorita.Add(new ClienteReceitaFavorita
                    {
                        ClienteId = cliente.UtilizadorId,
                        ReceitaId = id
                    });
                }
                else
                {
                    context.ClienteReceitaFavorita.Remove(favorito);
                }

                context.SaveChanges();
            }

            return RedirectToAction(actionName: "Detalhes", routeValues: new { id });
        }

        [HttpPost("[controller]/[action]/{id}/{numProcesso}")]
        public IActionResult Confecionar([FromRoute] int id, [FromRoute] int indiceProcesso)
        {
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

            if (indiceProcesso < 0)
                return RedirectToAction(actionName: "Detalhes", routeValues: new { id });

            if (indiceProcesso >= (receita.Processos as IList<Processo>).Count)
                return View(viewName: "TerminarConfecao");

            var (tecnicas, utensilios) = receita.GetTecnicasUtensilios();

            var viewModel = (
                Receita: receita,
                Tecnicas: tecnicas,
                Utensilios: utensilios,
                Favorita: ReceitaIsFavorita(receita.ReceitaId),
                Processo: indiceProcesso
                );

            return View(viewName: "ConfecionarReceita", model: viewModel);
        }

        private bool ReceitaIsFavorita(int idReceita)
        {
            return
                Autenticacao.GetUtilizadorAutenticado(this, context) is Cliente cliente &&
                context.ClienteReceitaFavorita.Find(cliente.UtilizadorId, idReceita) != null;
        }
    }
}
