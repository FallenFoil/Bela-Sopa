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
    public class CriarIngredienteController : Controller
    {
        private readonly BelaSopaContext context;

        public CriarIngredienteController(BelaSopaContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["tipo"] = "ingrediente";
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

            var ingrediente = new Ingrediente
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

            context.AdicionarIngrediente(ingrediente, bytesImagem);

            TempData["Success"] = "Ingrediente criado com sucesso.";
            return RedirectToAction();
        }
    }
}
