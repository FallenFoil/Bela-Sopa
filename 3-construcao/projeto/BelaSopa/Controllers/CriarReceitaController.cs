using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Assistente;
using BelaSopa.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BelaSopa.Controllers
{
    public class CriarReceitaController : Controller
    {
        private readonly BelaSopaContext context;

        public CriarReceitaController(BelaSopaContext context) {
            this.context = context;
        }

        public IActionResult CriarReceita(CriarReceitaViewModel form) {
            if (ModelState.IsValid) {
                Receita receita = new Receita();
                switch (form.DificuldadeStr) {
                    case "Fácil": receita.Dificuldade = Dificuldade.Facil;  break;
                    case "Médio": receita.Dificuldade = Dificuldade.Media; break;
                    case "Dificil": receita.Dificuldade = Dificuldade.Dificil; break;
                    default: break;
                }
                receita.Nome = form.NomeDeReceita;
                receita.MinutosPreparacao = form.Minutos;
                receita.NumeroDoses = form.Doses;
                List<string> nomesEtiquetas = new List<String>();
                foreach (Etiqueta rt in form.ReceitaEtiqueta) nomesEtiquetas.Add(rt.Nome);

                Processo processo = new Processo();
                foreach (TextoTarefa txtTarefa in form.Tarefas) {
                    processo.Tarefas.Add(new Tarefa {
                        Texto = new List<TextoTarefa> {
                            new TextoTarefa{
                                Texto = txtTarefa.Texto
                            }
                        }
                    });
                }
                if (form.Tarefas.Count > 0)
                    receita.Processos.Add(processo);
                receita.UtilizacoesIngredientes = form.UtilizacoesIngredientes;
                receita.ValoresNutricionais = form.ValorNutricionais;
                receita.Descricao = form.Descricao;
                try {
                    context.AdicionarReceita(receita, nomesEtiquetas, new byte[0]);
                    form = new CriarReceitaViewModel();
                    TempData["Success"] = "Receita adicionada com sucesso.";
                    return Index(form);
                } catch (Exception e) {
                    TempData["Error"] = "Não foi possivel adicionar a receita";
                    return Index(form);
                }
            }
            else {
                TempData["Error"] = "Não foi possivel adicionar a receita, verifique todos os campos";
                return Index(form);
            }
        }

        public IActionResult Index(CriarReceitaViewModel Receita) {
            if (Receita == null) {
                Receita = new CriarReceitaViewModel();
            }

            if(Receita.Etiquetas.Count == 0) {
                List<Etiqueta> ets = context.Etiqueta.ToList<Etiqueta>();
                foreach (Etiqueta et in ets) {
                    if(!Receita.ReceitaEtiqueta.Contains(et))
                        Receita.Etiquetas.Add(new SelectListItem { Value = et.EtiquetaId.ToString(), Text = et.Nome });
                }
            }



            return View(viewName: "CriarReceita", model: Receita);
        }

        public IActionResult NovoValorNutricional(CriarReceitaViewModel Receita) {
            Receita.ValorNutricionais.Add(new ValorNutricional());
            return ValoresNutricionais(Receita);
        }


        public IActionResult NovaEtiqueta(CriarReceitaViewModel Receita) {
            Receita.ReceitaEtiqueta.Add(new Etiqueta());
            return Index(Receita);
        }

        /*
        [HttpPost("[controller]/[action]/{idProcesso}")]
        public IActionResult NovaTarefa(CriarReceitaViewModel Receita, [FromRoute] int idProcesso) {
            if(idProcesso < 0 || Receita.Processos.Count-1 < idProcesso || Receita.Processos[idProcesso] == null) {
                TempData["Error"] = "Não foi possível criar uma nova Tarefa";
                return ProcessosETarefas(Receita);
            }
            Receita.Processos[idProcesso].Add("");

            return ProcessosETarefas(Receita);
        }

        public IActionResult NovoProcesso(CriarReceitaViewModel Receita) {
            List<string> tarefa = new List<string>();
            tarefa.Add("");
            Receita.Processos.Add(tarefa);
            return ProcessosETarefas(Receita);
        }*/

        public IActionResult NovaTarefa(CriarReceitaViewModel Receita) {
            Receita.Tarefas.Add(new TextoTarefa { Texto = "" });
            return ProcessosETarefas(Receita);
        }

        public IActionResult NovoIngrediente(CriarReceitaViewModel Receita) {
            Receita.UtilizacoesIngredientes.Add(new UtilizacaoIngrediente());
            return Ingredientes(Receita);
        }

        [HttpPost("[controller]/[action]/{num}")]
        public IActionResult RemoverIngrediente(CriarReceitaViewModel Receita, [FromRoute] int num) {
            Receita.UtilizacoesIngredientes.RemoveAt(num);
            return Ingredientes(Receita);
        }


        public IActionResult ValoresNutricionais(CriarReceitaViewModel Receita) {
            return View(viewName: "ValoresNutricionais", model: Receita);
        }
        public IActionResult Ingredientes(CriarReceitaViewModel Receita) {
            return View(viewName: "Ingredientes", model: Receita);
        }
        public IActionResult ProcessosETarefas(CriarReceitaViewModel Receita) {
            return View(viewName: "ProcessosETarefas", model: Receita);
        }

        private void SaveViewModel(CriarReceitaViewModel Receita) {
            TempData["form"] = Receita;
        }

        private CriarReceitaViewModel GetViewModel() {
            return TempData["form"] as CriarReceitaViewModel;
        }

    }
}
