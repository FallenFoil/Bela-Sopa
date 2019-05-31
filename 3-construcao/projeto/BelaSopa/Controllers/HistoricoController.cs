using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Assistente;
using BelaSopa.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BelaSopa.Controllers
{
    public class HistoricoController : Controller {
        private readonly BelaSopaContext context;

        public HistoricoController(BelaSopaContext context){
            this.context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            List<ClienteFinalizado> receitasFinalizadas = context.ClienteFinalizado
                                                        .Where(cf => cf.ClienteId == Autenticacao.GetUtilizadorAutenticado(this, context).UtilizadorId)
                                                        .ToList();

            return View(viewName: "Historico", model: receitasFinalizadas);
        }
    }
}
