using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Assistente;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BelaSopa.Controllers
{
    [Authorize]
    public class EmentaSemanalController : Controller
    {
        private readonly BelaSopaContext context;

        public EmentaSemanalController(BelaSopaContext context) {
            this.context = context;
        }

        public IActionResult Index()
        {
            IDictionary<DateTime, Receita> Ementa = new Dictionary<DateTime, Receita>();
            ClienteEmentaSemanal[] receitas = context.ClienteEmentaSemanal
                                                .Where(ces => ces.ClienteId == 1)
                                                .ToArray<ClienteEmentaSemanal>();
            foreach(ClienteEmentaSemanal ces in receitas)
                Ementa.Add(ces.Horario, ces.Receita);

            ViewData["Ementa"] = Ementa;

            return View(viewName: "EmentaSemanal");
        }
    }
}
