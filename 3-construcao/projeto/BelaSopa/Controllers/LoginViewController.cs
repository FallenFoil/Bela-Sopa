using Microsoft.AspNetCore.Mvc;

namespace BelaSopa.Controllers
{
    public class LoginViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public IActionResult UserLogin()
        {
            return View();
        }
        /*
        [HttpGet]
        public async Task<IActionResult> Logout(){
            await HttpContext.SignOutAsync();
            return RedirectToAction("index", "Home");
        }*/
    }
}
