using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BelaSopa.Models;
using BelaSopa.Models.Assistente;
using BelaSopa.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BelaSopa.Controllers
{
    [Authorize(Roles = "Cliente")]
    public class HomeController : Controller
    {
        private readonly BelaSopaDbContext context;

        public HomeController(BelaSopaDbContext context) {
            this.context = context;
        }

        public IActionResult Index()
        {
            HomeViewModel hvm = new HomeViewModel();
            hvm.Etiquetas = this.context.Etiqueta.ToArray<Etiqueta>();
            hvm.Receitas = this.context.Receita.ToArray<Receita>();

            return View(viewName: "Home", model: hvm);
        }
    }
}
