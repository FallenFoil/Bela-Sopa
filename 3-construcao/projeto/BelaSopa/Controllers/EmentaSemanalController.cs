using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Assistente;
using BelaSopa.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BelaSopa.Controllers
{
    [Authorize]
    public class EmentaSemanalController : Controller
    {
        private readonly BelaSopaContext context;

        public EmentaSemanalController(BelaSopaContext context) {
            this.context = context;
        }

        public IActionResult Index()
        {
            IDictionary<int, Receita> Ementa = new Dictionary<int, Receita>();
            ClienteEmentaSemanal[] receitas = context.ClienteEmentaSemanal
                                                .Where(ces => ces.ClienteId == Autenticacao.GetUtilizadorAutenticado(this, context).UtilizadorId)
                                                .ToArray<ClienteEmentaSemanal>();
            foreach (ClienteEmentaSemanal ces in receitas) {
                DataRefeicao dr = context.DataRefeicao.Find(ces.DataRefeicaoId);
                Receita r = context.Receita.Find(ces.ReceitaId);
                Ementa.Add(dr.DataRefeicaoId, r);
            }
            return View(viewName: "EmentaSemanal", model: Ementa);
        }

        public IActionResult RemoverReceita(int idHorario) {
            ClienteEmentaSemanal toRemove = context.ClienteEmentaSemanal.Where(ces =>
                ces.ClienteId == Autenticacao.GetUtilizadorAutenticado(this, context).UtilizadorId &&
                ces.DataRefeicaoId == ces.DataRefeicaoId
                ).FirstOrDefault();
            context.ClienteEmentaSemanal.Remove(toRemove);
            context.SaveChanges();
            return Index();
        }

        public IActionResult AdicionarReceita(int idDataRefeicao, int idReceita) {

            context.ClienteEmentaSemanal.Add(new ClienteEmentaSemanal(Autenticacao.GetUtilizadorAutenticado(this, context).UtilizadorId,
                                                                idReceita, idDataRefeicao ));
            context.SaveChanges();
            return Index();
        }

        public IActionResult VerReceitas(
           [FromQuery] string nome,
           [FromQuery] int? etiqueta,
           [FromQuery] Dificuldade? dificuldade,
           [FromQuery(Name = "data")] int Data
           ) {
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
                    Etiquetas: context.Etiqueta.ToList(),
                    Receitas: receitas.ToList(),
                    Data
                    );

                return View(viewName: "AdicionarReceita", model: viewModel);
            }

    }
}
