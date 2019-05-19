using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using App.Models.Assistente;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers {
    public class HomeController : Controller {
        public HomeController(BelaSopaContext context) {
            EtiquetaController = new EtiquetaController(context);
        }

        private EtiquetaController EtiquetaController { set; get; }
        [ViewData]
        public Etiqueta[] Etiquetas { set; get; }
        [ViewData]
        public String Testing { set; get; }
        public IActionResult Index() {
            ViewData["Message"] = "Hello2";
            Testing = "Hello";
            Etiquetas = EtiquetaController.Get();
            return View();
        }
    }
}
