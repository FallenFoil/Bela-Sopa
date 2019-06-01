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
using System.Security.Claims;
using System.Threading.Tasks;

namespace BelaSopa.Controllers
{
    [Authorize]
    public class ContaController : Controller
    {
        private readonly BelaSopaContext context;
        private bool r;

        public ContaController(BelaSopaContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {

            if (User.HasClaim(ClaimTypes.Role, Autenticacao.ROLE_CLIENTE))
            {
               var listaIngredientesExcluidos =
               context
               .Cliente
               .Include(c => c.ClienteExcluiIngrediente)
                   .ThenInclude(cei => cei.Ingrediente)
               .Single(c => c.UtilizadorId == Autenticacao.GetUtilizadorAutenticado(this, context).UtilizadorId)
               .ClienteExcluiIngrediente
               .Select(cei => cei.Ingrediente);


                List<string> ingredientes = new List<string>();

                foreach (Ingrediente x in listaIngredientesExcluidos)
                {
                    ingredientes.Add(x.Nome);
                }

                var favoritos = context.ClienteFavorito.Where(cf => cf.ClienteId == (Autenticacao.GetUtilizadorAutenticado(this, context) as Cliente).UtilizadorId).ToList<ClienteFavorito>();
                List<Receita> receitas = new List<Receita>();
                foreach (ClienteFavorito cf in favoritos)
                {
                    receitas.Add(context.Receita.Find(cf.ReceitaId));
                }
                var viewModel = (ingredientes,receitas, (Autenticacao.GetUtilizadorAutenticado(this, context) as Cliente)?.Email);

                return View(viewName: "VerDados", model: viewModel);
            }else{
                var viewModel = (new List<string>(), new List<Receita>(), (Autenticacao.GetUtilizadorAutenticado(this, context) as Utilizador)?.NomeDeUtilizador);
                return View(viewName: "VerDados", model: viewModel);
            }
        }

        public IActionResult TggFav(int? id)
        {
            if (id.HasValue)
            {

                int idCliente = Autenticacao.GetUtilizadorAutenticado(this, context).UtilizadorId;
                ClienteFavorito cf = new ClienteFavorito(idCliente, id.Value);
                context.ClienteFavorito.Remove(cf);
                context.SaveChanges();

                return Index();
            }
            else { return NotFound(); }
        }

        public bool IsFavorito(int idReceita)
        {
            bool Favorita = context.ClienteFavorito.Any(f => f.ReceitaId == idReceita &&
                                                            f.ClienteId == Autenticacao.GetUtilizadorAutenticado(this, context).UtilizadorId);
            return Favorita;
        }

        [HttpGet]
        public IActionResult AlterarPalavraPasse()
        {
            return View(viewName: "AlterarPalavraPasse");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AlterarPalavraPasse([Bind] AlterarPalavraPasseViewModel viewModel)
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
            await context.SaveChangesAsync();

            // redirecionar

            TempData["Sucesso"] = "Palavra-passe alterada com sucesso.";
            return RedirectToAction(actionName: "Index");
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
        public IActionResult Detalhes([FromRoute] int id)
        {

            int idCliente = Autenticacao.GetUtilizadorAutenticado(this, context).UtilizadorId;

            ClienteExcluiIngrediente aux = new ClienteExcluiIngrediente();

            aux.ClienteId = idCliente;
            aux.IngredienteId = id;

            context.ClienteExcluiIngrediente.Add(aux);
            context.SaveChanges();


            var listaIngredientesExcluidos =
                context
                .Cliente
                .Include(c => c.ClienteExcluiIngrediente)
                    .ThenInclude(cei => cei.Ingrediente)
                .Single(c => c.UtilizadorId == idCliente)
                .ClienteExcluiIngrediente
                .Select(cei => cei.Ingrediente);


            List<string> ingredientes = new List<string>();

            foreach (Ingrediente x in listaIngredientesExcluidos)
            {
               ingredientes.Add(x.Nome);
            }

            var favoritos = context.ClienteFavorito.Where(cf => cf.ClienteId == (Autenticacao.GetUtilizadorAutenticado(this, context) as Cliente).UtilizadorId).ToList<ClienteFavorito>();
            List<Receita> receitas = new List<Receita>();
            foreach (ClienteFavorito cf in favoritos)
            {
                receitas.Add(context.Receita.Find(cf.ReceitaId));
            }
            var viewModel = (ingredientes, receitas, (Autenticacao.GetUtilizadorAutenticado(this, context) as Cliente)?.Email);


            return View(viewName: "VerDados", model: viewModel);
        }


        [HttpGet]
        [Route("[controller]/[action]/{id}")]
        public IActionResult Remove([FromRoute] string id)
        {
            int idCliente = Autenticacao.GetUtilizadorAutenticado(this, context).UtilizadorId;
            ClienteExcluiIngrediente aux = new ClienteExcluiIngrediente();

            aux.ClienteId = idCliente;

            aux.IngredienteId = context.Ingrediente.Where(ingr => ingr.Nome.Equals(id)).FirstOrDefault().IngredienteId;

            if (aux!=null)
            {
                context.ClienteExcluiIngrediente.Remove(aux);
                context.SaveChanges();
            }


            var listaIngredientesExcluidos =
               context
               .Cliente
               .Include(c => c.ClienteExcluiIngrediente)
                   .ThenInclude(cei => cei.Ingrediente)
               .Single(c => c.UtilizadorId == idCliente)
               .ClienteExcluiIngrediente
               .Select(cei => cei.Ingrediente);


            List<string> ingredientes = new List<string>();

            foreach (Ingrediente x in listaIngredientesExcluidos)
            {
                ingredientes.Add(x.Nome);
            }

            var favoritos = context.ClienteFavorito.Where(cf => cf.ClienteId == (Autenticacao.GetUtilizadorAutenticado(this, context) as Cliente).UtilizadorId).ToList<ClienteFavorito>();
            List<Receita> receitas = new List<Receita>();
            foreach (ClienteFavorito cf in favoritos)
            {
                receitas.Add(context.Receita.Find(cf.ReceitaId));
            }
            var viewModel = (ingredientes, receitas, (Autenticacao.GetUtilizadorAutenticado(this, context) as Cliente)?.Email);


            return View(viewName: "VerDados", model: viewModel);


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
        public async Task<IActionResult> AlterarEmail([Bind] AlterarEmailViewModel viewModel)
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
            await context.SaveChangesAsync();

            // redirecionar

            TempData["Sucesso"] = "Endereço de e-mail alterado com sucesso.";
            return RedirectToAction(actionName: "Index");
        }
    }
}
