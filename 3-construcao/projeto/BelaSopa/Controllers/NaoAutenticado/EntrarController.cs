using BelaSopa.Models;
using BelaSopa.Models.BusinessModels.Utilizadores;
using BelaSopa.Models.ViewModels.NaoAutenticado;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BelaSopa.Controllers.NaoAutenticado
{
    public class EntrarController : Controller
    {
        private readonly BelaSopaDbContext context;

        public EntrarController(BelaSopaDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return
                await Autenticacao.RedirecionarSeAutenticado(this, this.context)
                ?? View(viewName: "../NaoAutenticado/Entrar");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Entrar([Bind] EntrarViewModel viewModel)
        {
            // validar dados

            if (!ModelState.IsValid)
            {
                // dados inválidos
                return View(viewName: "../NaoAutenticado/Entrar");
            }

            // verificar se utilizador existe

            var utilizador = this.context.GetUtilizador(viewModel.NomeDeUtilizador);

            if (utilizador == null)
            {
                // utilizador não existe
                TempData["Erro"] = "Utilizador não existe.";
                return View(viewName: "../NaoAutenticado/Entrar");
            }

            // verificar se palavra-passe está correta

            if (!utilizador.HashPalavraPasse.SequenceEqual(Utilizador.ComputarHashPalavraPasse(viewModel.PalavraPasse)))
            {
                // palavra-passe incorreta
                TempData["Erro"] = "Palavra-passe incorreta.";
                return View(viewName: "../NaoAutenticado/Entrar");
            }

            // autenticar utilizador e redirecionar

            return await Autenticacao.AutenticarUtilizador(this, utilizador);
        }

        [HttpGet]
        public IActionResult CriarConta()
        {
            return RedirectToAction(actionName: "Index", controllerName: "CriarConta");
        }
    }
}
