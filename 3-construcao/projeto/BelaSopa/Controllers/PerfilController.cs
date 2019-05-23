using BelaSopa.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BelaSopa.Controllers
{
    [Authorize(Roles = "Cliente")]
    public class PerfilController : Controller
    {
        private readonly BelaSopaDbContext context;

        public PerfilController(BelaSopaDbContext context)
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
