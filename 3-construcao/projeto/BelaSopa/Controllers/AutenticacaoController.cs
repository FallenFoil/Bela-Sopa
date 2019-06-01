using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Utilizadores;
using BelaSopa.Models.ViewModels;
using BelaSopa.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BelaSopa.Controllers
{
    public class AutenticacaoController : Controller
    {
        private readonly BelaSopaContext context;

        public AutenticacaoController(BelaSopaContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return
                await Autenticacao.RedirecionarSeAutenticado(this, this.context)
                ?? View(viewName: "Entrar");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind] EntrarViewModel viewModel)
        {
            // redirecionar utilizador se autenticado

            var r = await Autenticacao.RedirecionarSeAutenticado(this, this.context);

            if (r != null)
                return r;

            // validar dados

            if (!ModelState.IsValid)
            {
                // dados inválidos
                return View(viewName: "Entrar");
            }

            // verificar se utilizador existe

            var utilizador = this.context.GetUtilizador(viewModel.NomeDeUtilizador);

            if (utilizador == null)
            {
                // utilizador não existe
                TempData["Erro"] = "Utilizador não existe.";
                return View(viewName: "Entrar");
            }

            // verificar se palavra-passe está correta

            var hashPalavraPasse = Util.ComputarHashPalavraPasse(viewModel.PalavraPasse);

            if (!utilizador.HashPalavraPasse.SequenceEqual(hashPalavraPasse))
            {
                // palavra-passe incorreta
                TempData["Erro"] = "Palavra-passe incorreta.";
                return View(viewName: "Entrar");
            }

            // autenticar utilizador e redirecionar

            return await Autenticacao.AutenticarUtilizador(this, utilizador);
        }

        [HttpGet]
        public async Task<IActionResult> CriarConta()
        {
            return
                await Autenticacao.RedirecionarSeAutenticado(this, this.context)
                ?? View(viewName: "CriarConta");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarConta([Bind] CriarContaViewModel viewModel)
        {
            // redirecionar utilizador se autenticado

            var r = await Autenticacao.RedirecionarSeAutenticado(this, this.context);

            if (r != null)
                return r;

            // validar dados

            if (!ModelState.IsValid)
            {
                // dados inválidos
                return View(viewName: "CriarConta");
            }

            // verificar se nome de utilizador está disponível

            if (this.context.GetUtilizador(viewModel.NomeDeUtilizador) != null)
            {
                // nome de utilizador indisponível
                TempData["Erro"] = "Nome de utilizador indisponível.";
                return View(viewName: "CriarConta");
            }

            // registar cliente

            var cliente = new Cliente
            {
                NomeDeUtilizador = viewModel.NomeDeUtilizador,
                HashPalavraPasse = Util.ComputarHashPalavraPasse(viewModel.PalavraPasse),
                Email = viewModel.Email
            };

            this.context.Cliente.Add(cliente);
            this.context.SaveChanges();

            // redirecionar para view de autenticação

            TempData["Sucesso"] = "Conta criada com sucesso.";
            return View(
                viewName: "Entrar",
                model: new EntrarViewModel { NomeDeUtilizador = viewModel.NomeDeUtilizador }
                );
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Sair()
        {
            await Autenticacao.DesautenticarUtilizador(this);

            return RedirectToAction(actionName: "Index");
        }
    }
}
