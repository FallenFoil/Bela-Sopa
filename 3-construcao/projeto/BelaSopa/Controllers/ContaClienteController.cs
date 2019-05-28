using BelaSopa.Models;
using BelaSopa.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BelaSopa.Controllers
{
    [Authorize(Roles = Autenticacao.ROLES_CLIENTE)]
    public class ContaClienteController : Controller
    {
        private readonly BelaSopaContext context;

        public ContaClienteController(BelaSopaContext context)
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
