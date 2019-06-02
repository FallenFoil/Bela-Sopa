using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Assistente;
using BelaSopa.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        [HttpGet]
        public IActionResult Index()
        {
            var idCliente = Autenticacao.GetUtilizadorAutenticado(this, context).UtilizadorId;

            var refeicoes =
                context
                .RefeicaoEmentaSemanal
                .Where(res => res.ClienteId == idCliente)
                .Include(res => res.Receita)
                .ToDictionary(res => (res.DiaDaSemana, res.RefeicaoDoDia), res => res.Receita);

            var viewModel = (
                Refeicoes: refeicoes,
                IngredientesNecessarios: GetIngredientesNecessarios()
                );

            return View(viewName: "EmentaSemanal", model: viewModel);
        }

        [HttpGet]
        [Route("[controller]/[action]/{diaDaSemana}/{refeicaoDoDia}")]
        public IActionResult VerReceitas(
            [FromRoute] DiaDaSemana diaDaSemana,
            [FromRoute] RefeicaoDoDia refeicaoDoDia,
            [FromQuery] string nome,
            [FromQuery] int? etiqueta,
            [FromQuery] Dificuldade? dificuldade
            )
        {
            ViewData["Title"] = "Adicionar refeição à ementa semanal";
            ViewData["Action"] = "AdicionarRefeicao";
            ViewData["DiaDaSemana"] = diaDaSemana;
            ViewData["RefeicaoDoDia"] = refeicaoDoDia;

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

            return View(viewName: "~/Views/Receitas/ListaReceitas.cshtml", model: viewModel);
        }

        [HttpGet]
        [Route("[controller]/[action]/{diaDaSemana}/{refeicaoDoDia}/{id}")]
        public IActionResult AdicionarRefeicao(
            [FromRoute] DiaDaSemana diaDaSemana,
            [FromRoute] RefeicaoDoDia refeicaoDoDia,
            [FromRoute] int id
            )
        {
            var idCliente = Autenticacao.GetUtilizadorAutenticado(this, context).UtilizadorId;

            context.RefeicaoEmentaSemanal.Add(new RefeicaoEmentaSemanal
            {
                ClienteId = idCliente,
                DiaDaSemana = diaDaSemana,
                RefeicaoDoDia = refeicaoDoDia,
                ReceitaId = id
            });

            context.SaveChanges();

            return RedirectToAction(actionName: "Index");
        }

        [HttpGet]
        [Route("[controller]/[action]/{diaDaSemana}/{refeicaoDoDia}")]
        public IActionResult RemoverRefeicao(
            [FromRoute] DiaDaSemana diaDaSemana,
            [FromRoute] RefeicaoDoDia refeicaoDoDia
            )
        {
            var idCliente = Autenticacao.GetUtilizadorAutenticado(this, context).UtilizadorId;

            var refeicao =
                context
                .RefeicaoEmentaSemanal
                .Find(idCliente, diaDaSemana, refeicaoDoDia);

            if (refeicao != null)
            {
                context.RefeicaoEmentaSemanal.Remove(refeicao);
                context.SaveChanges();
            }

            return RedirectToAction(actionName: "Index");
        }

        private Dictionary<string, string> GetIngredientesNecessarios()
        {
            var idCliente = Autenticacao.GetUtilizadorAutenticado(this, context).UtilizadorId;

            var utilizacoesIngredientes =
                context
                .RefeicaoEmentaSemanal
                .Where(res => res.ClienteId == idCliente)
                .Include(res => res.Receita)
                    .ThenInclude(r => r.UtilizacoesIngredientes)
                    .ThenInclude(ui => ui.Ingrediente)
                .SelectMany(res => res.Receita.UtilizacoesIngredientes);

            var ingrQuantidade = new Dictionary<string, Dictionary<string, double>>();

            foreach (UtilizacaoIngrediente ui in utilizacoesIngredientes)
            {
                string[] splitUI = ui.Quantidade.Split(" ");
                if (ingrQuantidade.ContainsKey(ui.Nome))
                {
                    Dictionary<string, double> units = ingrQuantidade[ui.Nome];
                    string unit = "";
                    for (int i = 1; i < splitUI.Length; i++)
                    {
                        unit += splitUI[i] + " ";
                    }
                    if (splitUI[0].Equals("qb"))
                    {
                        units.Add(unit, 0);
                    }
                    else
                    {
                        double quantity = double.Parse(splitUI[0].Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture);
                        if (units.ContainsKey(unit))
                        {
                            units[unit] += quantity;
                        }
                        else units.Add(unit, quantity);
                    }
                }
                else
                {
                    var quantidades = new Dictionary<string, double>();

                    if (splitUI.Length >= 2)
                    {
                        string key = "";
                        for (int i = 1; i < splitUI.Length; i++)
                        {
                            key += splitUI[i] + " ";
                        }
                        try
                        {
                            quantidades.Add(key, double.Parse(splitUI[0].Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture));
                        }
                        catch (Exception)
                        {
                            throw new Exception(splitUI[0].Replace(',', '.'));
                        }
                    }
                    else if (splitUI.Length == 1)
                    {
                        if (splitUI[0].Equals("qb"))
                        {
                            quantidades.Add("qb", 0);
                        }
                        else
                        {
                            quantidades.Add("", double.Parse(splitUI[0].Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture));
                        }
                    }

                    ingrQuantidade.Add(ui.Nome, quantidades);
                }
            }

            var ingredientes = new Dictionary<string, string>();

            foreach (string ingrediente in ingrQuantidade.Keys)
            {
                string total = "";

                foreach (string unit in ingrQuantidade[ingrediente].Keys)
                {
                    if (unit.Equals("qb"))
                        total += "qb + ";
                    else if (unit.Equals(""))
                        total += ingrQuantidade[ingrediente][unit].ToString() + " un. + ";
                    else
                        total += ingrQuantidade[ingrediente][unit].ToString() + " " + unit + " + ";
                }

                total = total.Substring(0, total.Length - 2);
                ingredientes.Add(ingrediente, total);
            }

            return ingredientes;
        }
    }
}
