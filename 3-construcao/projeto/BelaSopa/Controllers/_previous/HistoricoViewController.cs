using Microsoft.AspNetCore.Mvc;

namespace BelaSopa.Controllers
{
    public class HistoricoViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
