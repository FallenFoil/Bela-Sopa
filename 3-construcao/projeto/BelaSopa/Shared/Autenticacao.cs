using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Utilizadores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BelaSopa.Shared
{
    public static class Autenticacao
    {
        public const string ROLE_ADMINISTRADOR = "Administrador";
        public const string ROLE_CLIENTE = "Cliente";

        private const string CONTROLLER_INICIAL_ADMINISTRADOR = "Utilizadores";
        private const string CONTROLLER_INICIAL_CLIENTE = "Receitas";

        public static async Task<IActionResult> AutenticarUtilizador(Controller controller, Utilizador utilizador)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, utilizador.NomeDeUtilizador),
                new Claim(ClaimTypes.Role, (utilizador is Cliente) ? ROLE_CLIENTE : ROLE_ADMINISTRADOR)
            };

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "login"));

            await controller.HttpContext.SignInAsync(claimsPrincipal);

            // redirecionar para página de receitas ou administração (consoante tipo de utilizador)

            return controller.RedirectToAction(
                actionName: "Index",
                controllerName: (utilizador is Cliente) ? CONTROLLER_INICIAL_CLIENTE : CONTROLLER_INICIAL_ADMINISTRADOR
                );
        }

        public static async Task DesautenticarUtilizador(Controller controller)
        {
            await controller.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public static Utilizador GetUtilizadorAutenticado(
            Controller controller,
            BelaSopaContext context
            )
        {
            // verificar se utilizador está autenticado

            if (controller.User.Identity.IsAuthenticated)
            {
                // obter nome de utilizador armazenado no cookie

                var nomeDeUtilizador = controller.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                if (nomeDeUtilizador != null)
                {
                    var utilizador = context.GetUtilizador(nomeDeUtilizador);

                    // verificar se utilizador existe

                    if (utilizador != null)
                        return utilizador;
                }
            }

            // não autenticado ou autenticação inválida

            return null;
        }

        public static async Task<IActionResult> RedirecionarSeAutenticado(
            Controller controller,
            BelaSopaContext context
            )
        {
            // verificar se utilizador está autenticado

            if (controller.User.Identity.IsAuthenticated)
            {
                var utilizador = GetUtilizadorAutenticado(controller, context);

                if (utilizador != null)
                {
                    // já autenticado, redirecionar

                    return controller.RedirectToAction(
                        actionName: "Index",
                        controllerName:
                            (utilizador is Cliente) ? CONTROLLER_INICIAL_CLIENTE : CONTROLLER_INICIAL_ADMINISTRADOR
                        );
                }

                // sessão inválida, remover autenticação

                await controller.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }

            // não autenticado

            return null;
        }
    }
}
