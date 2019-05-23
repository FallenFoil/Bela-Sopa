using BelaSopa.Models;
using BelaSopa.Models.Utilizadores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public async Task<IActionResult> Entrar([Bind] Credenciais credenciais)
        {
            // validar dados

            if (!ModelState.IsValid)
            {
                // dados inválidos
                return View(viewName: "../NaoAutenticado/Entrar");
            }

            // verificar se utilizador existe

            var utilizador = this.context.GetUtilizador(credenciais.NomeDeUtilizador);

            if (utilizador == null)
            {
                // utilizador não existe
                TempData["Erro"] = "Utilizador não existe.";
                return View(viewName: "../NaoAutenticado/Entrar");
            }

            // verificar se palavra-passe está correta

            if (!utilizador.HashPalavraPasse.SequenceEqual(credenciais.ComputarHashPalavraPasse()))
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
