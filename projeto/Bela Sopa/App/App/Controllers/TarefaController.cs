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

        // GET api/tarefa
        [HttpGet]
        public Tarefa[] get() {
            return  _context.Tarefa.ToArray<Tarefa>();
        }

        // GET api/tarefa/5
        [HttpGet("{codigo}")]
        public ActionResult get(int codigo) {
            Tarefa tar = _context.Tarefa.Find(codigo);
            if (tar == null) return NotFound();
            return Ok(tar);
        }

        // GET api/tarefa/getTecnicas/5
        [HttpGet("getTecnicas/{codigo}")]
        public ActionResult getTecnicas(int codigo){
            var tarefa = _context.Tarefa.Find(codigo);
            if (tarefa == null)
                return NotFound();
            var tecnicas = from tecnica in _context.Tecnica
                           join tarefaTecnica in _context.TarefaTecnica.Where(tt => tt.TarefaId == codigo)
                                on tecnica.TecnicaId equals tarefaTecnica.TecnicaId                          
                           select new {
                               tecnica.TecnicaId,
                               tecnica.Descricao,
                               tecnica.Nome,
                               tecnica.Link,
                               tecnica.ImagePath
                           };
            return Ok(tecnicas);
        }

        // GET api/tarefa/getIngredientes/5
        [HttpGet("getIngredientes/{codigo}")]
        public ActionResult getIngredientes(int codigo) {
            var tarefa = _context.Tarefa.Find(codigo);
            if (tarefa == null)
                return NotFound();
            var ingredientes = from ingrediente in _context.Ingrediente
                               join tarefaIngrediente in _context.TarefaIngrediente.Where(tt => tt.TarefaId == codigo)
                                    on ingrediente.IngredienteId equals tarefaIngrediente.IngredienteId
                               select new {
                                   ingrediente.IngredienteId,
                                   ingrediente.Descricao,
                                   ingrediente.Nome,
                                   ingrediente.Link,
                                   ingrediente.ImagePath
                               };
            return Ok(ingredientes);
        }

        // POST api/tecnica
        [HttpPost]
        public IActionResult Post([FromBody] Tarefa tar) {
            _context.Tarefa.Add(tar);
            _context.SaveChanges();
            return new CreatedResult($"/api/Tecnica/{tar.TarefaId}", tar);
        }

        // POST api/tarefa/addTecnica/1/1
        [HttpPost("addTecnica/{idTarefa}/{idTecnica}")]
        public IActionResult PostTecnica(int idTarefa, int idTecnica) {
            bool tartecn = _context.TarefaTecnica.Any(tt => tt.TecnicaId == idTecnica 
                                                        && tt.TarefaId == idTarefa);
            bool tecn = _context.Tecnica.Any(tecnica => tecnica.TecnicaId == idTecnica);
            bool tar = _context.Tarefa.Any(tarefa => tarefa.TarefaId == idTarefa);
            if (tartecn ) {
                return NotFound();
            }
            TarefaTecnica t = new TarefaTecnica();
            t.TarefaId = idTarefa;
            t.TecnicaId = idTecnica;
            _context.TarefaTecnica.Add(t);
            _context.SaveChanges();
            return Ok(t);
        }

        // DELETE api/tarefa/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            Tarefa t = _context.Tarefa.Find(id);
            if (t == null) {
                return NotFound();
            }

            _context.Tarefa.Remove(t);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
