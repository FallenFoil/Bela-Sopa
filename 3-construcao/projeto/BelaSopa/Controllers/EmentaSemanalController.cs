using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Assistente;
using BelaSopa.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BelaSopa.Controllers
{
    [Authorize(Roles = Autenticacao.ROLE_CLIENTE)]
    public class EmentaSemanalController : Controller
    {
        private readonly BelaSopaContext context;

        public EmentaSemanalController(BelaSopaContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            IDictionary<int, Receita> Ementa = new Dictionary<int, Receita>();
            ClienteEmentaSemanal[] receitas = context.ClienteEmentaSemanal
                                                .Where(ces => ces.ClienteId == Autenticacao.GetUtilizadorAutenticado(this, context).UtilizadorId)
                                                .ToArray<ClienteEmentaSemanal>();
            foreach (ClienteEmentaSemanal ces in receitas)
            {
                DataRefeicao dr = context.DataRefeicao.Find(ces.DataRefeicaoId);
                Receita r = context.Receita.Find(ces.ReceitaId);
                Ementa.Add(dr.DataRefeicaoId, r);
            }
            return View(viewName: "EmentaSemanal", model: Ementa);
        }

        public IActionResult RemoverReceita(int idHorario)
        {
            ClienteEmentaSemanal toRemove = context.ClienteEmentaSemanal.Where(ces =>
                ces.ClienteId == Autenticacao.GetUtilizadorAutenticado(this, context).UtilizadorId &&
                ces.DataRefeicaoId == ces.DataRefeicaoId
                ).FirstOrDefault();
            context.ClienteEmentaSemanal.Remove(toRemove);
            context.SaveChanges();
            return Index();
        }

        public IActionResult AdicionarReceita(int idDataRefeicao, int idReceita)
        {

            context.ClienteEmentaSemanal.Add(new ClienteEmentaSemanal(Autenticacao.GetUtilizadorAutenticado(this, context).UtilizadorId,
                                                                idReceita, idDataRefeicao));
            context.SaveChanges();
            return Index();
        }

        [HttpGet("[controller]/[action]/{Data}")]
        public IActionResult VerReceitas(
           [FromQuery] string nome,
           [FromQuery] int? etiqueta,
           [FromQuery] Dificuldade? dificuldade,
           [FromRoute] int Data
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
                Etiquetas: context.Etiqueta.ToList(),
                Receitas: receitas.ToList(),
                Data
                );

            return View(viewName: "AdicionarReceita", model: viewModel);
        }

        public IActionResult GerarListaIngredientes() {
            Receita[] receitasEmenta = context.ClienteEmentaSemanal
                                        .Where(ces => ces.ClienteId == Autenticacao.GetUtilizadorAutenticado(this, context).UtilizadorId)
                                        .Join(context.Receita, ementa => ementa.ReceitaId, receita => receita.ReceitaId, (ementa, receita) => receita)
                                        .ToArray<Receita>();
            Dictionary<string, string> ingredientes = new Dictionary<string, string>();
            Dictionary<string, Dictionary<string, int>> ingrQuantidade = new Dictionary<string, Dictionary<string, int>>();
            foreach (Receita r in receitasEmenta) {
                ICollection<UtilizacaoIngrediente> ingrs = context.UtilizacaoIngrediente.Where(utilU => utilU.ReceitaId == r.ReceitaId).ToArray<UtilizacaoIngrediente>();
                foreach(UtilizacaoIngrediente ui in ingrs) {
                    string[] splitUI = ui.Quantidade.Split(" ");
                    if (ingrQuantidade.ContainsKey(ui.Nome)) {
                        Dictionary<string, int> units = ingrQuantidade[ui.Nome];
                        string unit = "";
                        for (int i = 1; i < splitUI.Length; i++) {
                            unit += splitUI[i] + " ";
                        }
                        if (splitUI[0].Equals("qb")) {
                            units.Add(unit, 0);
                        } else {
                            int quantity = Int32.Parse(splitUI[0]);
                            if (units.ContainsKey(unit)) {
                                units[unit] += quantity;
                            } else units.Add(unit, quantity);
                        }
                    }
                    else {
                        Dictionary<string, int> quantidades = new Dictionary<string, int>();
                        if (splitUI.Length >= 2) {
                            string key = "";
                            for (int i = 1; i < splitUI.Length; i++) {
                                key += splitUI[i] + " ";
                            } 
                            quantidades.Add(key, Int32.Parse(splitUI[0]));
                        } else if (splitUI.Length == 1) {
                            if (splitUI[0].Equals("qb")) {
                                quantidades.Add("qb", 0);
                            } else {
                                quantidades.Add("", Int32.Parse(splitUI[0]));
                            }
                        }
                        ingrQuantidade.Add(ui.Nome, quantidades);
                    }
                }
            }
            foreach(string ingrediente in ingrQuantidade.Keys) {
                string total = "";
                foreach (string unit in ingrQuantidade[ingrediente].Keys) {
                    if (unit.Equals("qb")) {
                        total += "qb + ";
                    } else if (unit.Equals("")) total += ingrQuantidade[ingrediente][unit].ToString() + " un. + ";
                    else total += ingrQuantidade[ingrediente][unit].ToString() + " " + unit + " + ";
                }
                total = total.Substring(0, total.Length - 2);
                ingredientes.Add(ingrediente, total);
            }
            return View(viewName: "ListaDeIngredientes", model: ingredientes);
        }

    }
}
