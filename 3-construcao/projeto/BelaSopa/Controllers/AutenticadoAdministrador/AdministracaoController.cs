using BelaSopa.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BelaSopa.Controllers.AutenticadoAdministrador
{
    [Authorize(Roles = "Administrador")]
    public class AdministracaoController : Controller
    {
        private readonly BelaSopaDbContext context;

        public AdministracaoController(BelaSopaDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(viewName: "../AutenticadoAdministrador/Administracao/Index");
        }

        [HttpGet]
        public async Task<IActionResult> Sair()
        {
            await Autenticacao.DesautenticarUtilizador(this);

            return RedirectToAction(actionName: "Index", controllerName: "Entrar");
        }
    }
}
