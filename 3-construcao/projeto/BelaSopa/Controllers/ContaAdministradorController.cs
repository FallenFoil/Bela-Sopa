using BelaSopa.Models;
using BelaSopa.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BelaSopa.Controllers
{
    [Authorize(Roles = Autenticacao.ROLES_ADMINISTRADOR)]
    public class ContaAdministradorController : Controller
    {
        private readonly BelaSopaContext context;

        public ContaAdministradorController(BelaSopaContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(viewName: "VerDados");
        }
    }
}
