using BelaSopa.Models;
using BelaSopa.Models.Utilizadores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BelaSopa.Controllers
{
    public static class Autenticacao
    {
        public static async Task<IActionResult> AutenticarUtilizador(Controller controller, Utilizador utilizador)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, utilizador.NomeDeUtilizador),
                new Claim(ClaimTypes.Role, (utilizador is Cliente) ? "Cliente" : "Administrador")
            };

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "login"));

            await controller.HttpContext.SignInAsync(claimsPrincipal);

            // redirecionar para página de receitas ou administração (consoante tipo de utilizador)

            return controller.RedirectToAction(
                actionName: "Index",
                controllerName: (utilizador is Cliente) ? "Receitas" : "Administracao"
                );
        }

        public static async Task DesautenticarUtilizador(Controller controller)
        {
            await controller.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public static async Task<IActionResult> RedirecionarSeAutenticado(
            Controller controller,
            BelaSopaDbContext context
            )
        {
            // verificar se utilizador já está autenticado

            if (controller.User.Identity.IsAuthenticated)
            {
                // obter nome de utilizador armazenado no cookie

                var nomeDeUtilizador = controller.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                if (nomeDeUtilizador != null)
                {
                    var utilizador = context.GetUtilizador(nomeDeUtilizador);

                    // verificar se utilizador existe

                    if (utilizador != null)
                    {
                        // já autenticado, redirecionar

                        return controller.RedirectToAction(
                            actionName: "Index",
                            controllerName: (utilizador is Cliente) ? "Receitas" : "Administracao"
                            );
                    }
                }

                // sessão inválida, remover autenticação

                await controller.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }

            // não autenticado

            return null;
        }
    }
}
