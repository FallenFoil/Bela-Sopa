using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Assistente;
using BelaSopa.Models.DomainModels.Utilizadores;
using BelaSopa.Models.ViewModels;
using BelaSopa.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace BelaSopa.Controllers
{
    [Authorize]
    public class CriarReceitaController : Controller
    {
        private readonly BelaSopaContext context;

        public CriarReceitaController(BelaSopaContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult CriarReceita(CriarReceitaViewModel form, List<IFormFile> imagem)
        {
            if (ModelState.IsValid)
            {
                Receita receita = new Receita();

                byte[] imageArr = null;
                if (imagem.Count > 0) {
                    using (var memoryStream = new MemoryStream()) {
                        imagem[0].CopyToAsync(memoryStream);
                        imageArr = memoryStream.ToArray();
                    }
                }
                if (imageArr == null) {
                    TempData["Error"] = "Selecione uma imagem para a receita";
                    return Index(form);
                }

                if(form.Processos.Count == 0) {
                    TempData["Error"] = "Não pode adicionar uma receita sem processos";
                    return Index(form);
                }

                switch (form.DificuldadeStr)
                {
                    case "Fácil": receita.Dificuldade = Dificuldade.Facil; break;
                    case "Médio": receita.Dificuldade = Dificuldade.Media; break;
                    case "Dificil": receita.Dificuldade = Dificuldade.Dificil; break;
                    default: break;
                }
                receita.Nome = form.NomeDeReceita;
                receita.MinutosPreparacao = form.Minutos;
                receita.NumeroDoses = form.Doses;
                List<string> nomesEtiquetas = new List<String>();

                for(int i = 0; i < form.Processos.Count; i++) {
                    if (form.Processos[i].Count == 0) {
                        TempData["Error"] = "Não pode adicionar um processo sem tarefas (Processo: " + (i+1).ToString() + " )" ;
                        return Index(form);
                    }
                    for(int j = 0; j < form.Processos[i].Count; j++) { 
                        if (form.Processos[i][j].Equals("")) {
                            TempData["Error"] = "Não pode existir uma tarefa vazia (Processo: " + (i+1).ToString() + ", Tarefa:" + (j+1).ToString() + " )";
                            return Index(form);
                        }
                        receita.Processos.Add(new Processo {
                            Tarefas = new List<Tarefa> {
                                new Tarefa {
                                    Texto = new List<TextoTarefa> {
                                    new TextoTarefa{
                                        Texto = form.Processos[i][j]
                                        }
                                    }
                                }
                            }
                        });
                    }
                }

                if (form.UtilizacoesIngredientes.Count == 0) {
                    TempData["Error"] = "Selecione um ingrediente no mínimo";
                    return Index(form);
                }
                for(int i = 0; i < form.UtilizacoesIngredientes.Count; i++) {
                    form.UtilizacoesIngredientes[i].Quantidade = form.Quantidades[i].ToString() + " " + form.UtilizacoesIngredientes[i].Quantidade;
                }
                receita.UtilizacoesIngredientes = form.UtilizacoesIngredientes;
                for(int i = 0; i < form.NomeValorNutricionais.Count; i++) {
                    receita.ValoresNutricionais.Add(new ValorNutricional {
                        Nome = form.NomeValorNutricionais[i],
                        Dose = form.PorDose[i].ToString() + " " + form.UnidadeValorNutricionais[i],
                        PercentagemVdrAdulto = form.PercentagemVdrAdulto[i]
                    });
                }
                receita.Descricao = form.Descricao;

                if (Autenticacao.GetUtilizadorAutenticado(this, context) is Cliente cliente)
                    receita.ClienteId = cliente.UtilizadorId;

                context.AdicionarReceita(receita, form.ReceitaEtiqueta, imageArr);
                form = new CriarReceitaViewModel();
                TempData["Success"] = "Receita adicionada com sucesso.";
                return RedirectToAction("Index", "Receitas");
            }
            else
            {
                TempData["Error"] = "Não foi possivel adicionar a receita, verifique todos os campos";
                return Index(form);
            }
        }

        public IActionResult Index(CriarReceitaViewModel Receita)
        {
            if (Receita == null)
            {
                Receita = new CriarReceitaViewModel();
            }

            if (Receita.Etiquetas.Count == 0)
            {
                List<Etiqueta> ets = context.Etiqueta.ToList<Etiqueta>();
                foreach (Etiqueta et in ets)
                {
                    Receita.Etiquetas.Add(new SelectListItem { Value = et.Nome, Text = et.Nome });
                }
            }



            return View(viewName: "CriarReceita", model: Receita);
        }

        public IActionResult NovoValorNutricional(CriarReceitaViewModel Receita)
        {
            Receita.NomeValorNutricionais.Add("");
            Receita.PorDose.Add(0);
            Receita.PercentagemVdrAdulto.Add(0);
            Receita.UnidadeValorNutricionais.Add("");
            
            return ValoresNutricionais(Receita);
        }


        [HttpPost("[controller]/[action]/{num}")]
        public IActionResult RemoverValorNutricional(CriarReceitaViewModel Receita, [FromRoute] int num)
        {
            ModelState.Clear();
            Receita.NomeValorNutricionais.RemoveAt(num);
            Receita.PorDose.RemoveAt(num);
            Receita.PercentagemVdrAdulto.RemoveAt(num);
            Receita.UnidadeValorNutricionais.RemoveAt(num);

            return ValoresNutricionais(Receita);
        }


        public IActionResult NovaEtiqueta(CriarReceitaViewModel Receita)
        {
            List<Etiqueta> etiquetas = context.Etiqueta.ToList<Etiqueta>();
            if (Receita.ReceitaEtiqueta.Count == etiquetas.Count) {
                TempData["Error"] = "Não existem mais etiquetas";
            } else {
                Receita.ReceitaEtiqueta.Add("");
            } 

            return Index(Receita);
        }

        [HttpPost("[controller]/[action]/{num}")]
        public IActionResult RemoverEtiqueta(CriarReceitaViewModel Receita, [FromRoute] int num)
        {
            ModelState.Clear();
            if (num < 0 || num >= Receita.ReceitaEtiqueta.Count)
            {
                TempData["Error"] = "Não foi possivel remover a etiqueta";
                return Index(Receita);
            }
            Receita.ReceitaEtiqueta.RemoveAt(num);
            return Index(Receita);
        }



        
        [HttpPost("[controller]/[action]/{idProcesso}")]
        public IActionResult NovaTarefa(CriarReceitaViewModel Receita, [FromRoute] int idProcesso) {
            if(idProcesso < 0 || Receita.Processos.Count-1 < idProcesso || Receita.Processos[idProcesso] == null) {
                TempData["Error"] = "Não foi possível criar uma nova Tarefa";
                return ProcessosETarefas(Receita);
            }
            Receita.Processos[idProcesso].Add("");

            return ProcessosETarefas(Receita);
        }

        [HttpPost("[controller]/[action]/{processo}/{tarefa}")]
        public IActionResult RemoverTarefa(CriarReceitaViewModel Receita, [FromRoute] int processo, [FromRoute] int tarefa)
        {
            ModelState.Clear();

            if (processo < 0 || processo >= Receita.Processos.Count)
            {
                TempData["Error"] = "Não foi possivel remover a etiqueta";
                return ProcessosETarefas(Receita);
            }

            List<string> p = Receita.Processos[processo];

            if (tarefa < 0 || tarefa >= p.Count)
            {
                TempData["Error"] = "Não foi possivel remover a etiqueta";
                return ProcessosETarefas(Receita);
            }

            p.RemoveAt(tarefa); ;

            return ProcessosETarefas(Receita);
        }

        public IActionResult NovoProcesso(CriarReceitaViewModel Receita) {
            List<string> tarefa = new List<string>();
            tarefa.Add("");
            Receita.Processos.Add(tarefa);
            return ProcessosETarefas(Receita);
        }

        [HttpPost("[controller]/[action]/{processo}")]
        public IActionResult RemoverProcesso(CriarReceitaViewModel Receita, [FromRoute] int processo)
        {
            ModelState.Clear();
            if (processo < 0 || processo >= Receita.Processos.Count)
            {
                TempData["Error"] = "Não foi possivel remover a etiqueta";
                return ProcessosETarefas(Receita);
            }

            Receita.Processos.RemoveAt(processo);

            return ProcessosETarefas(Receita);
        }

        public IActionResult NovoIngrediente(CriarReceitaViewModel Receita)
        {
            Receita.UtilizacoesIngredientes.Add(new UtilizacaoIngrediente());
            Receita.Quantidades.Add(new Int32());
            return Ingredientes(Receita);
        }

        [HttpPost("[controller]/[action]/{num}")]
        public IActionResult RemoverIngrediente(CriarReceitaViewModel Receita, [FromRoute] int num)
        {
            if (num < 0 || num >= Receita.UtilizacoesIngredientes.Count)
            {
                TempData["Error"] = "Não foi possivel remover o ingrediente";
                return Ingredientes(Receita);
            }
            ModelState.Clear();
            Receita.UtilizacoesIngredientes.RemoveAt(num);
            Receita.Quantidades.RemoveAt(num);
            return Ingredientes(Receita);
        }


        public IActionResult ValoresNutricionais(CriarReceitaViewModel Receita)
        {
            return View(viewName: "ValoresNutricionais", model: Receita);
        }
        public IActionResult Ingredientes(CriarReceitaViewModel Receita)
        {
            return View(viewName: "Ingredientes", model: Receita);
        }
        public IActionResult ProcessosETarefas(CriarReceitaViewModel Receita)
        {
            return View(viewName: "ProcessosETarefas", model: Receita);
        }

        [HttpGet("[controller]/[action]/{idReceita}")]
        public IActionResult EditarReceita([FromRoute] int idReceita) {
            Receita r = context.Receita.Find(idReceita);
            if (r == null) return NotFound();
            CriarReceitaViewModel crvm = new CriarReceitaViewModel();

            crvm.NomeDeReceita = r.Nome;
            crvm.Descricao = r.Descricao;
            crvm.Minutos = r.MinutosPreparacao;
            crvm.Doses = r.NumeroDoses;
            if(r.Dificuldade == Dificuldade.Facil) {
                crvm.DificuldadeStr = "Fácil";
            } else if(r.Dificuldade == Dificuldade.Media){
                crvm.DificuldadeStr = "Médio";
            } else if(crvm.Dificuldade == Dificuldade.Dificil) {
                crvm.DificuldadeStr = "Dificil";
            }
             
            
            return Index(crvm);
        }

    }
}
