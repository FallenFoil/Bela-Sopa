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
            ViewData["tipo"] = "técnica";
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

            var tecnica = new Tecnica
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

            context.AdicionarTecnica(tecnica, bytesImagem);

            TempData["Success"] = "Técnica criada com sucesso.";
            return RedirectToAction();
        }
    }
}
