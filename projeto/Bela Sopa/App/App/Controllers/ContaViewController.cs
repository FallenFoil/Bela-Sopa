using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class ContaViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}