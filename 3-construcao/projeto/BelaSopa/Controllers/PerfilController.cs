using BelaSopa.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BelaSopa.Controllers
{
    [Authorize(Roles = Util.ROLES_CLIENTE)]
    public class PerfilController : Controller
    {
        private readonly BelaSopaContext context;

        public PerfilController(BelaSopaContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(viewName: "Perfil");
        }
    }
}
