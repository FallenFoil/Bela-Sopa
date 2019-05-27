using Microsoft.AspNetCore.Mvc;

namespace BelaSopa.Controllers
{
    public class VisualizacaoReceitaViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
