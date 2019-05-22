using BelaSopa.Models;
using Microsoft.AspNetCore.Mvc;


namespace BelaSopa.Controllers
{

    public class RegisterViewController : Controller
    {


        private readonly BelaSopaDbContext _context;

        public RegisterViewController(BelaSopaDbContext context)
        {
            _context = context;
        }


        // Post api/user -> inecabado
        [HttpPost]
        public IActionResult RegisterUser([Bind] Cliente user)
        {
            if (ModelState.IsValid)
            {
                bool RegistrationStatus = true; //cliente controller da post Post(user);
                if (RegistrationStatus)
                {
                    ModelState.Clear();
                    TempData["Success"] = "Registration Successful!";
                }
                else
                {
                    TempData["Fail"] = "This User ID already exists. Registration Failed.";
                }
            }
            return new CreatedResult($"/api/Cliente/{user.Nome}", user);

        }





        public IActionResult Index()
        {
            return View();
        }


    }
}
