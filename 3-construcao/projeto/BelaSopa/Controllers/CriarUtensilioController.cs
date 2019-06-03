using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Assistente;
using BelaSopa.Models.ViewModels;
using BelaSopa.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        [HttpPost]
        public IActionResult adiciona(CriarUtensilioViewModel form, List<IFormFile> imagem)
        {
            if (ModelState.IsValid)
            {
                Utensilio util = new Utensilio();

                util.Nome = form.NomeDoUtensilio;
                util.Descricao = form.DescricaoDoUtensilio;
                util.Texto = form.TextoDoUtensilio;

                byte[] imageArr = null;
                if (imagem.Count > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        imagem[0].CopyToAsync(memoryStream);
                        imageArr = memoryStream.ToArray();
                    }
                }
                if (imageArr == null)
                {
                    TempData["Error"] = "Selecione uma imagem para a receita";
                    return Index(form);
                }

                // try {
                context.AdicionarUtensilio(util,imageArr);
                form = new CriarUtensilioViewModel();
                TempData["Success"] = "Receita adicionada com sucesso.";
                return Index(form);
                //} catch (Exception e) {
                //  TempData["Error"] = "Não foi possivel adicionar a receita";
                //   return Index(form);
                // }
            }
            else
            {
                TempData["Error"] = "Não foi possivel adicionar a receita, verifique todos os campos";
                return Index(form);
            }
        }

        public IActionResult Index(CriarUtensilioViewModel Util)
        {
            if (Util == null)
            {
                Util = new CriarUtensilioViewModel();
            }

            return View(viewName: "CriarUtensilio");
        }
    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
}
