using BelaSopa.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BelaSopa.Controllers
{
    [Authorize(Roles = Util.ROLES_CLIENTE)]
    public class HomeController : Controller
    {
        private readonly BelaSopaContext context;

        public HomeController(BelaSopaContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = (
                Etiquetas: context.Etiqueta.ToList(),
                Receitas: context.Receita.ToList()
                );

            return View(viewName: "Home", model: viewModel);
        }
    }
}
