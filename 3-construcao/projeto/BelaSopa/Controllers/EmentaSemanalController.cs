using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Assistente;
using BelaSopa.Models.DomainModels.Utilizadores;
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
            ViewData["Title"] = "Adicionar receita à ementa semanal";
            ViewData["Action"] = "AdicionarRefeicao";
            ViewData["DiaDaSemana"] = diaDaSemana;
            ViewData["RefeicaoDoDia"] = refeicaoDoDia;
            ViewData["ShowBackButton"] = true;

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
                receitas = receitas.Where(
                    r => r.ClienteId == null || r.ClienteId == cliente.UtilizadorId
                    );

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

            var receita = context.Receita.Find(id);

            if (receita == null)
                return NotFound();

            if (Autenticacao.GetUtilizadorAutenticado(this, context) is Cliente cliente)
            {
                if (receita.ClienteId != null && receita.ClienteId != cliente.UtilizadorId)
                    return NotFound();
            }

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
                string[] splitUi = ui.Quantidade.Split();

                if (ingrQuantidade.ContainsKey(ui.Nome))
                {
                    var units = ingrQuantidade[ui.Nome];

                    var unit = "";

                    for (int i = 1; i < splitUi.Length; i++)
                        unit += splitUi[i] + " ";

                    if (splitUi[0].ToLower() == "qb")
                    {
                        if(!units.ContainsKey("qb"))
                            units.Add("qb", 0);
                    }
                    else
                    {

                        try {
                        var quantity = double.Parse(
                            splitUi[0].Replace(',', '.'),
                            NumberStyles.Any,
                            CultureInfo.InvariantCulture
                            );

                        if (units.ContainsKey(unit))
                            units[unit] += quantity;

                        else units.Add(unit, quantity);
                        } catch (Exception) { }
                    }
                }
                else
                {
                    var quantidades = new Dictionary<string, double>();

                    if (splitUi.Length >= 2)
                    {
                        var key = "";

                        for (int i = 1; i < splitUi.Length; i++)
                            key += splitUi[i] + " ";

                        try {
                            quantidades.Add(
                                key,
                                double.Parse(splitUi[0].Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture)
                                );
                        } catch (Exception) {

                        }
                    }
                    else if (splitUi.Length == 1)
                    {
                        if (splitUi[0].ToLower() == "qb")
                        {
                            quantidades.Add("qb", 0);
                        }
                        else
                        {
                            var quantity = 0.0;
                            try {
                                quantity = double.Parse(
                                    splitUi[0].Replace(',', '.'),
                                    NumberStyles.Any,
                                    CultureInfo.InvariantCulture
                                    );
                            } catch (Exception) {
                                quantity = 0;
                            }
                        }
                    }

                    ingrQuantidade.Add(ui.Nome, quantidades);
                }
            }

            var ingredientes = new Dictionary<string, string>();

            foreach (var ingrediente in ingrQuantidade.Keys)
            {
                string total = "";

                foreach (var unit in ingrQuantidade[ingrediente].Keys)
                {
                    if (unit.Equals("qb"))
                        total += "qb + ";
                    else if (unit.Equals(""))
                        total += ingrQuantidade[ingrediente][unit].ToString() + " un. + ";
                    else
                        total += ingrQuantidade[ingrediente][unit].ToString() + " " + unit + " + ";
                }

                if(total.Length - 2 > 0)
                    total = total.Substring(0, total.Length - 2);
                ingredientes.Add(ingrediente, total);
            }

            return ingredientes;
        }
    }
}
