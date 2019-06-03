using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Assistente;
using BelaSopa.Models.ViewModels;
using BelaSopa.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BelaSopa.Controllers
{
    [Authorize(Roles = Autenticacao.ROLE_ADMINISTRADOR)]
    public class CriarTecnicaController : Controller
    {
        private readonly BelaSopaContext context;

        public CriarTecnicaController(BelaSopaContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(viewName: "CriarTecnica");
        }

        [HttpPost]
        public IActionResult Index(CriarTecnicaViewModel viewModel, List<IFormFile> imagem)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Dados inválidos.";
                return View(viewName: "CriarTecnica");
            }

            var tecnica = new Tecnica
            {
                
                Nome = viewModel.Nome,
                Descricao = viewModel.Descricao,
                Texto = viewModel.Texto
            };

            var bytesImagem = Util.FormFilesToByteArray(imagem);

            if (bytesImagem.Length == 0)
            {
                TempData["Error"] = "Selecione uma imagem para o utensílio.";
                return View(viewName: "CriarTecnica");
            }

            context.AdicionarTecnica(tecnica, bytesImagem);

            return RedirectToAction(actionName: "Index", controllerName: "Tecnicas");
        }
    }
}
