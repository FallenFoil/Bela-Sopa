using BelaSopa.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BelaSopa.Controllers
{
    [Authorize]
    public class LojasController : Controller
    {
        private readonly BelaSopaContext context;

        public LojasController(BelaSopaContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(viewName: "LojasProximas");
        }
    }
}
