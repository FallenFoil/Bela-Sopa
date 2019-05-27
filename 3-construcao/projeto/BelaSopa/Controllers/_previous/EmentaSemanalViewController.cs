using Microsoft.AspNetCore.Mvc;

namespace BelaSopa.Controllers
{
    public class EmentaSemanalViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
