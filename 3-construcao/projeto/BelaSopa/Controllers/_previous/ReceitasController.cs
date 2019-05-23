using BelaSopa.Models;
using BelaSopa.Models.Assistente;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BelaSopa.Controllers.AutenticadoCliente
{
    [Authorize(Roles = "Cliente")]
    public class ReceitasController : Controller
    {
        public ReceitasController(BelaSopaDbContext context)
        {
            EtiquetaController = new EtiquetaController(context);
            ReceitaController = new ReceitaController(context);
        }

        private EtiquetaController EtiquetaController { set; get; }
        private ReceitaController ReceitaController { set; get; }
        [ViewData]
        public Etiqueta[] Etiquetas { set; get; }
        [ViewData]
        public Receita[] Receitas { set; get; }

        public IActionResult Index()
        {
            Receitas = ReceitaController.Get();
            Etiquetas = EtiquetaController.Get();
            return View();
        }
    }
}
