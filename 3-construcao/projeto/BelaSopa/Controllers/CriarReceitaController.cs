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

        private void CriarReceita(CriarReceitaViewModel form) {
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
            IEnumerable<string> etiquetas = nomesEtiquetas.AsEnumerable();

            receita.ValoresNutricionais = form.ValorNutricionais;
            receita.Descricao = form.Descricao;

            context.AdicionarReceita(receita, etiquetas);
        }

        public IActionResult Index(CriarReceitaViewModel Receita) {
            if (Receita == null) {
                Receita = new CriarReceitaViewModel();
            }

            if (ModelState.IsValid) {
                //CriarReceita(Receita);
                return View(viewName: "CriarReceita", model: Receita);
            }

           
                List<Etiqueta> ets = context.Etiqueta.ToList<Etiqueta>();
                foreach (Etiqueta et in ets) {
                    Receita.Etiquetas.Add(new SelectListItem { Value = et.EtiquetaId.ToString(), Text = et.Nome });
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
