using BelaSopa.Models;
using Microsoft.AspNetCore.Mvc;

namespace BelaSopa.Controllers
{
    public class CriarContaController : Controller
    {
        private readonly BelaSopaDbContext context;

        public CriarContaController(BelaSopaDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View(viewName: "Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CriarConta([Bind] DadosCliente dadosCliente)
        {
            return BadRequest();
        }
    }
}
