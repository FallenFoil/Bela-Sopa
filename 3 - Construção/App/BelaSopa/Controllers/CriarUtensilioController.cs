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
    public class CriarUtensilioController : Controller
    {
        private readonly BelaSopaContext context;

        public CriarUtensilioController(BelaSopaContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["tipo"] = "utensílio";
            return View(viewName: "/Views/CriarItu/Index.cshtml");
        }

        [HttpPost]
        public IActionResult Index(CriarItuViewModel viewModel, List<IFormFile> imagem)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Dados inválidos.";
                return Index();
            }

            var utensilio = new Utensilio
            {
                Nome = viewModel.Nome,
                Descricao = viewModel.Descricao,
                Texto = viewModel.Texto
            };

            var bytesImagem = Util.FormFilesToByteArray(imagem);

            if (bytesImagem.Length == 0)
            {
                TempData["Error"] = "Selecione uma imagem.";
                return Index();
            }

            context.AdicionarUtensilio(utensilio, bytesImagem);

            TempData["Success"] = "Utensílio criado com sucesso.";
            return RedirectToAction();
        }
    }
}
