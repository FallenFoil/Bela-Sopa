using Microsoft.AspNetCore.Mvc;

namespace BelaSopa.Controllers
{
    public class ContaViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
