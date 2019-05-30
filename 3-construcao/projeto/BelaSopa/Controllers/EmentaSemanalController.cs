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
    [Authorize]
    public class EmentaSemanalController : Controller
    {
        private readonly BelaSopaContext context;

        public EmentaSemanalController(BelaSopaContext context) {
            this.context = context;
        }

        public IActionResult Index()
        {
            IDictionary<TimeSpan, Receita> Ementa = new Dictionary<TimeSpan, Receita>();
            ClienteEmentaSemanal[] receitas = context.ClienteEmentaSemanal
                                                .Where(ces => ces.ClienteId == Autenticacao.GetUtilizadorAutenticado(this, context).UtilizadorId)
                                                .ToArray<ClienteEmentaSemanal>();
            foreach(ClienteEmentaSemanal ces in receitas)
                Ementa.Add(ces.Horario, ces.Receita);

            return View(viewName: "EmentaSemanal", model: Ementa);
        }

        public IActionResult RemoverReceita(TimeSpan horario) {
            ClienteEmentaSemanal toRemove = context.ClienteEmentaSemanal.Where(ces =>
                ces.ClienteId == Autenticacao.GetUtilizadorAutenticado(this, context).UtilizadorId &&
                ces.Horario.Equals(horario)
                ).First();
            context.ClienteEmentaSemanal.Remove(toRemove);
            context.SaveChanges();
            return Index();
        }
    }
}
