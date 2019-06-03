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
    [Authorize(Roles = Autenticacao.ROLE_CLIENTE)]
    public class HistoricoController : Controller {
        private readonly BelaSopaContext context;

        public HistoricoController(BelaSopaContext context){
            this.context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            List<ClienteReceitaFinalizada> receitasFinalizadas = context.ClienteReceitaFinalizada
                                                        .Where(cf => cf.ClienteId == Autenticacao.GetUtilizadorAutenticado(this, context).UtilizadorId)
                                                        .ToList();

            return View(viewName: "Historico", model: receitasFinalizadas);
        }
    }
}
