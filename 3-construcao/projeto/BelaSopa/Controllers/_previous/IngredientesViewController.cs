using Microsoft.AspNetCore.Mvc;

namespace BelaSopa.Controllers
{
    public class IngredientesViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
