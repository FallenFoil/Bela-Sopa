using BelaSopa.Models;
using BelaSopa.Models.Assistente;
using BelaSopa.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BelaSopa.Controllers
{
    [Authorize(Roles = Util.ROLES_CLIENTE)]
    public class HomeController : Controller
    {
        private readonly BelaSopaDbContext context;

        public HomeController(BelaSopaDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            HomeViewModel hvm = new HomeViewModel();
            hvm.Etiquetas = this.context.Etiqueta.ToArray<Etiqueta>();
            hvm.Receitas = this.context.Receita.ToArray<Receita>();

            return View(viewName: "Home", model: hvm);
        }
    }
}
