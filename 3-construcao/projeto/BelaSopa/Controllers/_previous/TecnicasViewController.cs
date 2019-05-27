using Microsoft.AspNetCore.Mvc;

namespace BelaSopa.Controllers
{
    public class TecnicasViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
