using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Utilizadores;
using BelaSopa.Models.ViewModels;
using BelaSopa.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BelaSopa.Controllers
{
    [Authorize(Roles = Autenticacao.ROLE_ADMINISTRADOR)]
    public class UtilizadoresController : Controller
    {
        private readonly BelaSopaContext context;

        public UtilizadoresController(BelaSopaContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = (
                Administradores: context.Administrador.ToList(),
                Clientes: context.Cliente.ToList()
            );

            return View(viewName: "ListaUtilizadores", model: viewModel);
        }

        [HttpGet]
        [Route("[controller]/[action]/{nomeDeUtilizador}")]
        public IActionResult RemoverUtilizador([FromRoute] string nomeDeUtilizador)
        {
            if (nomeDeUtilizador != User.Identity.Name)
            {
                var utilizador = context.GetUtilizador(nomeDeUtilizador);

                if (utilizador != null)
                {
                    context.Remove(utilizador);
                    context.SaveChanges();
                }
            }

            TempData["Sucesso"] = "Utilizador removido com sucesso.";
            return RedirectToAction(actionName: "Index");
        }


        [HttpGet]
        [Route("[controller]/[action]/{utilizadorId}")]
        public IActionResult RemoverUtil([FromRoute] int utilizadorId)
        {
            if (utilizadorId == Autenticacao.GetUtilizadorAutenticado(this,context).UtilizadorId)
            {
                Cliente aux = (Cliente) context.Cliente.Select(c => c.UtilizadorId == utilizadorId);

                if (aux != null)
                {
                    context.Remove(aux);
                    context.SaveChanges();
                }
            }

            return View(viewName: "Entrar");
        }

        [HttpGet]
        public IActionResult AdicionarAdministrador()
        {
            return View(viewName: "AdicionarAdministrador");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdicionarAdministrador([Bind] AdicionarAdministradorViewModel viewModel)
        {
            // validar dados

            if (!ModelState.IsValid)
            {
                // dados inválidos
                return View(viewName: "AdicionarAdministrador");
            }

            // verificar se nome de utilizador está disponível

            if (context.GetUtilizador(viewModel.NomeDeUtilizador) != null)
            {
                // nome de utilizador indisponível
                TempData["Erro"] = "Nome de utilizador indisponível.";
                return View(viewName: "AdicionarAdministrador");
            }

            // registar cliente

            var administrador = new Administrador
            {
                NomeDeUtilizador = viewModel.NomeDeUtilizador,
                HashPalavraPasse = Util.ComputarHashPalavraPasse(viewModel.PalavraPasse)
            };

            context.Administrador.Add(administrador);
            context.SaveChanges();

            // redirecionar para view de autenticação

            TempData["Sucesso"] = "Administrador criado com sucesso.";
            return RedirectToAction(actionName: "Index");
        }
    }
}
