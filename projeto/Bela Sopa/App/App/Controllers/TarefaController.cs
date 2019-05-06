using App.Models;
using App.Models.Assistente;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers{

    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : Controller {

        private readonly BelaSopaContext _context;

        public TarefaController(BelaSopaContext context) {
            Console.WriteLine("Test");
            _context = context;
        }

        [HttpGet]
        public Tarefa[] get() {
            return new Tarefa[] { new Tarefa() { TarefaId = 1, Descricao = "Teste", Tempo = 10 } };// _context.Tarefa.ToArray<Tarefa>();
        }
        /*
        [HttpGet("{codigo}")]
        public ActionResult get(int codigo){
            Console.WriteLine(codigo);
            var tarefa = _context.Tarefas.Find(codigo);
            if (tarefa == null)
                return NotFound();
           
            return Ok(tarefa);
        }*/
    }
}
