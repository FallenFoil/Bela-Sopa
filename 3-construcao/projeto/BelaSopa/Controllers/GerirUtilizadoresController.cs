using BelaSopa.Models;
using BelaSopa.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BelaSopa.Controllers.AutenticadoAdministrador
{
    [Authorize(Roles = "Administrador")]
    public class GerirUtilizadoresController : Controller
    {
        private readonly BelaSopaDbContext context;

        public GerirUtilizadoresController(BelaSopaDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new ListaUtilizadoresViewModel
            {
                Administradores = this.context.Administradores.ToList(),
                Clientes = this.context.Clientes.ToList()
            };

            return View(viewName: "ListaUtilizadores", model: viewModel);
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

            return RedirectToAction(actionName: "Index");
        }
    }
}
