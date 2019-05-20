using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BelaSopa.Models;
using BelaSopa.Models.Assistente;
using Microsoft.AspNetCore.Mvc;

namespace BelaSopa.Controllers {
    public class HomeController : Controller {
        public HomeController(BelaSopaContext context) {
            EtiquetaController = new EtiquetaController(context);
            ReceitaController = new ReceitaController(context);
        }

        private EtiquetaController EtiquetaController { set; get; }
        private ReceitaController ReceitaController { set; get; }
        [ViewData]
        public Etiqueta[] Etiquetas { set; get; }
        [ViewData]
        public Receita[] Receitas { set; get; }

        public IActionResult Index() {
            Receitas = ReceitaController.Get();
            Etiquetas = EtiquetaController.Get();
            return View();
        }
    }
}
