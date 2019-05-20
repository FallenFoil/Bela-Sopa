using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BelaSopa.Models;
using BelaSopa.Models.Assistente;
using Microsoft.AspNetCore.Mvc;

namespace BelaSopa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessoController : Controller
    {
        private readonly BelaSopaContext _context;

        public ProcessoController(BelaSopaContext context) {
            _context = context;
        }

        // GET /api/processo
        [HttpGet]
        public Processo[] get() {
             return _context.Processo.ToArray<Processo>();
        }

        // GET api/processo/5
        [HttpGet("{codigo}")]
        public ActionResult get(int codigo) {
            var processo = _context.Processo.Find(codigo);
            if (processo == null)
                return NotFound();
            return Ok(processo);
        }

        // GET api/processo/tarefa/1
        [HttpGet("tarefa/{idProcesso}")]
        public IActionResult getTarefas(int idProcesso) {
            bool exists = _context.Processo.Any<Processo>(p => p.ProcessoId == idProcesso);
            if (!exists) return NotFound();
            var tarefas =   from tarefa in _context.Tarefa
                             join processoTarefa in _context.ProcessoTarefa.Where(pt => pt.ProcessoId == idProcesso)
                                on tarefa.TarefaId equals processoTarefa.TarefaId
                             select new {
                                tarefa.TarefaId,
                                tarefa.Tempo,
                                tarefa.Descricao,
                            };
            return Ok(tarefas);
        }


        // POST /api/processo
        [HttpPost]
        public IActionResult post([FromBody] Processo p) {
            _context.Processo.Add(p);
            _context.SaveChanges();
             return new CreatedResult($"/api/processo/{p.ProcessoId}", p);
        }

        // POST api/processo/tarefa/5/1
        [HttpPost("tarefa/{idProcesso}/{idTarefa}")]
        public IActionResult postTarefa(int idProcesso, int idTarefa) {
            bool exists = _context.ProcessoTarefa.Any(tt => tt.ProcessoId == idProcesso
                                                       && tt.TarefaId == idTarefa);
            Tarefa tarefa = _context.Tarefa.Find(idTarefa);
            Processo processo = _context.Processo.Find(idProcesso);

            if (exists || tarefa == null || processo == null) {
                return NotFound();
            }
            ProcessoTarefa pt = new ProcessoTarefa(idProcesso, idTarefa);
            _context.ProcessoTarefa.Add(pt);
            processo.Tempo += tarefa.Tempo;
            _context.SaveChanges();
            return Ok(pt);
        }

        // DELETE api/processo/tarefa/5/1
        [HttpDelete("tarefa/{idProcesso}/{idTarefa}")]
        public IActionResult DeleteTarefa(int idProcesso, int idTarefa) {
            ProcessoTarefa processoTarefa = new ProcessoTarefa(idProcesso, idTarefa);
            _context.ProcessoTarefa.Remove(processoTarefa);

            Processo processo = _context.Processo.Find(idProcesso);
            Tarefa tarefa = _context.Tarefa.Find(idTarefa);
            //Pelo invariante de inserção nunca deve ser nulo, mas por prevenção o "if" foi posto
            if(processo != null && tarefa != null)
                processo.Tempo -= tarefa.Tempo;

            _context.SaveChanges();
            return Ok();
        }



            public IActionResult Index()
        {
            return View();
        }
    }
}