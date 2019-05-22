using BelaSopa.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BelaSopa.Controllers.Administrador
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
            return View(viewName: "Index");
        }

        [HttpGet]
        public async Task<IActionResult> Sair()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(actionName: "Index", controllerName: "Autenticacao");
        }
    }
}
