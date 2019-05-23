using BelaSopa.Models;
using BelaSopa.Models.Utilizadores;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BelaSopa.Controllers.NaoAutenticado
{
    public class CriarContaController : Controller
    {
        private readonly BelaSopaDbContext context;

        public CriarContaController(BelaSopaDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return
                await Autenticacao.RedirecionarSeAutenticado(this, this.context)
                ?? View(viewName: "../NaoAutenticado/CriarConta");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarConta([Bind] DadosCliente dadosCliente)
        {
            // validar dados

            if (!ModelState.IsValid)
            {
                // dados inválidos
                return View(viewName: "../NaoAutenticado/CriarConta");
            }

            // verificar se nome de utilizador está disponível

            if (this.context.GetUtilizador(dadosCliente.NomeDeUtilizador) != null)
            {
                // nome de utilizador indisponível
                TempData["Erro"] = "Nome de utilizador indisponível.";
                return View(viewName: "../NaoAutenticado/CriarConta");
            }

            // registar cliente

            var cliente = new Cliente
            {
                NomeDeUtilizador = dadosCliente.NomeDeUtilizador,
                HashPalavraPasse = dadosCliente.ComputarHashPalavraPasse(),
                Email = dadosCliente.Email
            };

            this.context.Clientes.Add(cliente);

            // autenticar cliente e redirecionar

            return await Autenticacao.AutenticarUtilizador(this, cliente);
        }

        [HttpGet]
        public IActionResult Voltar()
        {
            return RedirectToAction(actionName: "Index", controllerName: "Entrar");
        }
    }
}
