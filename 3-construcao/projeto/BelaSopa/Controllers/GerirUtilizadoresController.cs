using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Utilizadores;
using BelaSopa.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BelaSopa.Controllers
{
    [Authorize(Roles = Util.ROLES_ADMINISTRADOR)]
    public class GerirUtilizadoresController : Controller
    {
        private readonly BelaSopaContext context;

        public GerirUtilizadoresController(BelaSopaContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = (
                Administradores: this.context.Administradores.ToList(),
                Clientes: this.context.Clientes.ToList()
            );

            return View(viewName: "ListaDeUtilizadores", model: viewModel);
        }

        [HttpGet]
        [Route("[controller]/[action]/{nomeDeUtilizador}")]
        public IActionResult RemoverUtilizador(string nomeDeUtilizador)
        {
            if (nomeDeUtilizador != User.Identity.Name)
            {
                var utilizador = this.context.GetUtilizador(nomeDeUtilizador);

                if (utilizador != null)
                {
                    this.context.Remove(utilizador);
                    this.context.SaveChanges();
                }
            }

            TempData["Sucesso"] = "Utilizador removido com sucesso.";
            return RedirectToAction(actionName: "Index");
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

            if (this.context.GetUtilizador(viewModel.NomeDeUtilizador) != null)
            {
                // nome de utilizador indisponível
                TempData["Erro"] = "Nome de utilizador indisponível.";
                return View(viewName: "AdicionarAdministrador");
            }

            // registar cliente

            var administrador = new Administrador
            {
                NomeDeUtilizador = viewModel.NomeDeUtilizador,
                HashPalavraPasse = Utilizador.ComputarHashPalavraPasse(viewModel.PalavraPasse)
            };

            this.context.Administradores.Add(administrador);
            this.context.SaveChanges();

            // redirecionar para view de autenticação

            TempData["Sucesso"] = "Administrador criado com sucesso.";
            return RedirectToAction(actionName: "Index");
        }
    }
}
