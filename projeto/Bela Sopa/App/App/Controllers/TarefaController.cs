using App.Models.Assistente;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers{

    [Route("api/[controller]")]
    public class TarefaController : Controller {

        private readonly TarefaContext _context;

        public TarefaController(TarefaContext context) {
            _context = context;
        }

        [HttpGet]
        public Tarefa[] get() {
            return _context.Tarefa.ToArray<Tarefa>();
        }
    }
}
