using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BelaSopa.Controllers.AutenticadoCliente
{
    public class TecnicasViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}