using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using App.Models;
using App.Models.Assistente;
using App.Models.Utilizadores;
using App.Controllers;


namespace App.Controllers
{
              
        public class RegisterViewController : Controller
        {

       
        private readonly BelaSopaContext _context;

        public RegisterViewController(BelaSopaContext context)
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