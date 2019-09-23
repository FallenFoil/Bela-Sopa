using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Assistente;
using BelaSopa.Models.DomainModels.Utilizadores;
using BelaSopa.Models.ViewModels;
using BelaSopa.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BelaSopa.Controllers
{
    [Authorize]
    public class ContaController : Controller
    {
        private readonly BelaSopaContext context;

        public ContaController(BelaSopaContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var utilizador = Autenticacao.GetUtilizadorAutenticado(this, context);

            string email = null;
            List<Receita> receitasFavoritas = null;
            List<Ingrediente> ingredientesExcluidos = null;

            if (utilizador is Cliente cliente)
            {
                email = cliente.Email;

                receitasFavoritas =
                    context
                    .ClienteReceitaFavorita
                    .Where(crf => crf.ClienteId == cliente.UtilizadorId)
                    .Include(crf => crf.Receita)
                    .Select(crf => crf.Receita)
                    .ToList();

                ingredientesExcluidos =
                    context
                    .ClienteExcluiIngrediente
                    .Where(cei => cei.ClienteId == cliente.UtilizadorId)
                    .Include(cei => cei.Ingrediente)
                    .Select(cei => cei.Ingrediente)
                    .ToList();
            }

            var viewModel = (
                Email: email,
                ReceitasFavoritas: receitasFavoritas,
                IngredientesExcluidos: ingredientesExcluidos
                );

            return View(viewName: "VerDados", model: viewModel);
        }

        [HttpGet]
        public IActionResult AlterarPalavraPasse()
        {
            return View(viewName: "AlterarPalavraPasse");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AlterarPalavraPasse([Bind] AlterarPalavraPasseViewModel viewModel)
        {
            // validar dados

            if (!ModelState.IsValid)
            {
                // dados inválidos
                return View(viewName: "AlterarPalavraPasse");
            }

            // obter utilizador

            var utilizador = Autenticacao.GetUtilizadorAutenticado(this, context);

            // validar palavra-passe atual

            var hashPalavraPasseAtual = Util.ComputarHashPalavraPasse(viewModel.PalavraPasseAtual);

            if (!utilizador.HashPalavraPasse.SequenceEqual(hashPalavraPasseAtual))
            {
                // palavra-passe incorreta
                TempData["Erro"] = "Palavra-passe atual incorreta.";
                return View(viewName: "AlterarPalavraPasse");
            }

            // alterar palavra-passe

            utilizador.HashPalavraPasse = Util.ComputarHashPalavraPasse(viewModel.NovaPalavraPasse);
            context.SaveChanges();

            // redirecionar

            TempData["Sucesso"] = "Palavra-passe alterada com sucesso.";
            return RedirectToAction(actionName: "Index");
        }

        [Authorize(Roles = Autenticacao.ROLE_CLIENTE)]
        [HttpGet]
        public IActionResult AlterarEmail()
        {
            return View(viewName: "AlterarEmail");
        }

        [Authorize(Roles = Autenticacao.ROLE_CLIENTE)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AlterarEmail([Bind] AlterarEmailViewModel viewModel)
        {
            // validar dados

            if (!ModelState.IsValid)
            {
                // dados inválidos
                return View(viewName: "AlterarEmail");
            }

            // obter utilizador

            var cliente = Autenticacao.GetUtilizadorAutenticado(this, context) as Cliente;

            // alterar email

            cliente.Email = viewModel.NovoEmail;
            context.SaveChanges();

            // redirecionar

            TempData["Sucesso"] = "Endereço de e-mail alterado com sucesso.";
            return RedirectToAction(actionName: "Index");
        }

        [Authorize(Roles = Autenticacao.ROLE_CLIENTE)]
        [HttpGet]
        public async Task<IActionResult> RemoverConta()
        {
            var cliente = Autenticacao.GetUtilizadorAutenticado(this, context) as Cliente;

            await Autenticacao.DesautenticarUtilizador(this);

            context.Cliente.Remove(cliente);
            context.SaveChanges();

            return RedirectToAction(actionName: "Index", controllerName: "Autenticacao");
        }

        [HttpGet]
        public IActionResult ListaIngredientes([FromQuery] string nome)
        {
            ViewData["nome"] = nome;

            // obter ingredientes

            IQueryable<Ingrediente> ingredientes = context.Ingrediente;

            if (nome != null)
                ingredientes = ingredientes.Where(ingrediente => Util.FuzzyContains(ingrediente.Nome, nome));

            ingredientes = ingredientes.OrderBy(ingrediente => ingrediente.Nome);

            var viewModel = ingredientes.ToList();

            // criar view model e devolver view

            return View(viewName: "ListaIngredientes", model: viewModel);
        }

        [HttpGet]
        [Route("[controller]/[action]/{id}")]
        public IActionResult AdicionarIngredienteExcluido([FromRoute] int id)
        {
            int idCliente = Autenticacao.GetUtilizadorAutenticado(this, context).UtilizadorId;

            context.ClienteExcluiIngrediente.Add(new ClienteExcluiIngrediente
            {
                ClienteId = idCliente,
                IngredienteId = id
            });

            context.SaveChanges();

            return RedirectToAction(actionName: "Index");
        }

        [HttpGet]
        [Route("[controller]/[action]/{id}")]
        public IActionResult RemoverIngredienteExcluido([FromRoute] int id)
        {
            if (Autenticacao.GetUtilizadorAutenticado(this, context) is Cliente cliente)
            {
                var exclusao = context.ClienteExcluiIngrediente.Find(cliente.UtilizadorId, id);

                if (exclusao != null)
                {
                    context.ClienteExcluiIngrediente.Remove(exclusao);
                    context.SaveChanges();
                }
            }

            return RedirectToAction(actionName: "Index");
        }

        [HttpGet]
        [Route("[controller]/[action]/{id}")]
        public IActionResult RemoverReceitaFavorita([FromRoute] int id)
        {
            if (Autenticacao.GetUtilizadorAutenticado(this, context) is Cliente cliente)
            {
                var favorito = context.ClienteReceitaFavorita.Find(cliente.UtilizadorId, id);

                if (favorito != null)
                {
                    context.ClienteReceitaFavorita.Remove(favorito);
                    context.SaveChanges();
                }
            }

            return RedirectToAction(actionName: "Index");
        }
    }
}
