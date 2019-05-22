using BelaSopa.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BelaSopa.Controllers
{
    public class AutenticacaoController : Controller
    {
        private readonly BelaSopaDbContext context;

        public AutenticacaoController(BelaSopaDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // verificar se utilizador já está autenticado

            if (User.Identity.IsAuthenticated)
            {
                // obter nome de utilizador armazenado no cookie

                var nomeDeUtilizador = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                if (nomeDeUtilizador != null)
                {
                    var utilizador = Utilizador.GetComNomeDeUtilizador(this.context, nomeDeUtilizador);

                    // verificar se utilizador existe

                    if (utilizador != null)
                    {
                        // já autenticado, redirecionar

                        return RedirectToAction(
                            actionName: "Index",
                            controllerName: (utilizador is Cliente) ? "Receitas" : "Administracao"
                            );
                    }
                }

                // sessão inválida, remover autenticação

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }

            // não autenticado

            return View(viewName: "Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Entrar([Bind] Credenciais credenciais)
        {
            // validar dados

            if (!ModelState.IsValid)
            {
                // dados inválidos
                return View(viewName: "Index");
            }

            // obter utilizador por nome de utilizador (cliente ou administrador)

            var utilizador = (Utilizador)
                this.context.Clientes.SingleOrDefault(c => c.NomeDeUtilizador == credenciais.NomeDeUtilizador) ??
                this.context.Administradores.SingleOrDefault(a => a.NomeDeUtilizador == credenciais.NomeDeUtilizador);

            // verificar se utilizador existe

            if (utilizador == null)
            {
                // utilizador não existe
                TempData["ErroAutenticacao"] = "Utilizador não existe.";
                return View(viewName: "Index");
            }

            // verificar se palavra-passe está correta

            if (!utilizador.HashPalavraPasse.SequenceEqual(credenciais.ComputarHashPalavraPasse()))
            {
                // palavra-passe incorreta
                TempData["ErroAutenticacao"] = "Palavra-passe incorreta.";
                return View(viewName: "Index");
            }

            // criar cookie

            var claimsPrincipal = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, utilizador.NomeDeUtilizador),
                        new Claim(ClaimTypes.Role, (utilizador is Cliente) ? "Cliente" : "Administrador")
                    },
                    "login"
                    )
                );

            await HttpContext.SignInAsync(claimsPrincipal);

            // redirecionar para página de receitas ou administração (consoante tipo de utilizador)

            return RedirectToAction(
                actionName: "Index",
                controllerName: (utilizador is Cliente) ? "Receitas" : "Administracao"
                );
        }

        [HttpGet]
        public IActionResult CriarConta()
        {
            return RedirectToAction(actionName: "Index", controllerName: "CriarConta");
        }
    }
}
