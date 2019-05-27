using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Assistente;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BelaSopa.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : Controller
    {

        private readonly BelaSopaContext _context;

        public TarefaController(BelaSopaContext context)
        {
            _context = context;
        }

        // GET api/tarefa
        [HttpGet]
        public Tarefa[] get()
        {
            return _context.Tarefa.ToArray<Tarefa>();
        }

        // GET api/tarefa/5
        [HttpGet("{codigo}")]
        public ActionResult get(int codigo)
        {
            Tarefa tar = _context.Tarefa.Find(codigo);
            if (tar == null) return NotFound();
            return Ok(tar);
        }

        // GET api/tarefa/tecnicas/5
        [HttpGet("tecnica/{codigo}")]
        public ActionResult getTecnicas(int codigo)
        {
            var tarefa = _context.Tarefa.Find(codigo);
            if (tarefa == null)
                return NotFound();
            var tecnicas = from tecnica in _context.Tecnica
                           join tarefaTecnica in _context.TarefaTecnica.Where(tt => tt.TarefaId == codigo)
                                on tecnica.TecnicaId equals tarefaTecnica.TecnicaId
                           select new
                           {
                               tecnica.TecnicaId,
                               tecnica.Descricao,
                               tecnica.Nome,
                               tecnica.Link,
                               tecnica.ImagePath
                           };
            return Ok(tecnicas);
        }

        // GET api/tarefa/ingrediente/5
        [HttpGet("ingrediente/{codigo}")]
        public ActionResult getIngredientes(int codigo)
        {
            var tarefa = _context.Tarefa.Find(codigo);
            if (tarefa == null)
                return NotFound();
            var ingredientes = from ingrediente in _context.Ingrediente
                               join tarefaIngrediente in _context.TarefaIngrediente.Where(tt => tt.TarefaId == codigo)
                                    on ingrediente.IngredienteId equals tarefaIngrediente.IngredienteId
                               select new
                               {
                                   ingrediente.IngredienteId,
                                   ingrediente.Descricao,
                                   ingrediente.Nome,
                                   ingrediente.Link,
                                   ingrediente.ImagePath
                               };
            return Ok(ingredientes);
        }

        // GET api/tarefa/utensilio/5
        [HttpGet("utensilio/{codigo}")]
        public ActionResult getUtensilios(int codigo)
        {
            var tarefa = _context.Tarefa.Find(codigo);
            if (tarefa == null)
                return NotFound();
            var utensilios = from utensilio in _context.Utensilio
                             join tarefaUtensilio in _context.TarefaUtensilio.Where(tt => tt.TarefaId == codigo)
                                  on utensilio.UtensilioId equals tarefaUtensilio.UtensilioId
                             select new
                             {
                                 utensilio.UtensilioId,
                                 utensilio.Descricao,
                                 utensilio.Nome,
                                 utensilio.Link,
                                 utensilio.ImagePath
                             };
            return Ok(utensilios);
        }

        // POST api/tarefa
        [HttpPost]
        public IActionResult Post([FromBody] Tarefa tar)
        {
            _context.Tarefa.Add(tar);
            _context.SaveChanges();
            return new CreatedResult($"/api/tarefa/{tar.TarefaId}", tar);
        }

        // POST api/tarefa/tecnica/1/1
        [HttpPost("tecnica/{idTarefa}/{idTecnica}")]
        public IActionResult PostTecnica(int idTarefa, int idTecnica)
        {
            bool tartecn = _context.TarefaTecnica.Any(tt => tt.TecnicaId == idTecnica
                                                        && tt.TarefaId == idTarefa);
            bool tecn = _context.Tecnica.Any(tecnica => tecnica.TecnicaId == idTecnica);
            bool tar = _context.Tarefa.Any(tarefa => tarefa.TarefaId == idTarefa);
            if (tartecn || !tecn || !tar)
            {
                return NotFound();
            }
            TarefaTecnica t = new TarefaTecnica(idTarefa, idTecnica);
            _context.TarefaTecnica.Add(t);
            _context.SaveChanges();
            return Ok(t);
        }

        // POST api/tarefa/utensilio/1/1
        [HttpPost("utensilio/{idTarefa}/{idUtensilio}")]
        public IActionResult PostUtensilio(int idTarefa, int idUtensilio)
        {
            bool tartecn = _context.TarefaUtensilio.Any(tt => tt.UtensilioId == idUtensilio
                                                        && tt.TarefaId == idTarefa);
            bool uten = _context.Utensilio.Any(utensilio => utensilio.UtensilioId == idUtensilio);
            bool tar = _context.Tarefa.Any(tarefa => tarefa.TarefaId == idTarefa);
            if (tartecn || !uten || !tar)
            {
                return NotFound();
            }
            TarefaUtensilio t = new TarefaUtensilio(idTarefa, idUtensilio);
            _context.TarefaUtensilio.Add(t);
            _context.SaveChanges();
            return Ok(t);
        }


        // POST api/tarefa/ingrediente/1/1
        [HttpPost("ingrediente/{idTarefa}/{idIngrediente}")]
        public IActionResult PostIngrediente(int idTarefa, int idIngrediente)
        {
            bool tartecn = _context.TarefaIngrediente.Any(tt => tt.IngredienteId == idIngrediente
                                                        && tt.TarefaId == idTarefa);
            bool ing = _context.Ingrediente.Any(ingr => ingr.IngredienteId == idIngrediente);
            bool tar = _context.Tarefa.Any(tarefa => tarefa.TarefaId == idTarefa);
            if (tartecn || !ing || !tar)
            {
                return NotFound();
            }
            TarefaIngrediente t = new TarefaIngrediente(idTarefa, idIngrediente);
            _context.TarefaIngrediente.Add(t);
            _context.SaveChanges();
            return Ok(t);
        }

        // DELETE api/tarefa/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Tarefa t = _context.Tarefa.Find(id);
            if (t == null)
            {
                return NotFound();
            }
            ProcessoTarefa processoTarefa = _context.ProcessoTarefa.Where<ProcessoTarefa>(pt => pt.TarefaId == id).First();
            Processo processo = _context.Processo.Find(processoTarefa.ProcessoId);

            _context.Tarefa.Remove(t);
            processo.Tempo -= t.Tempo;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE api/tarefa/ingrediente
        [HttpDelete("ingrediente/{idTarefa}/{idIngrediente}")]
        public IActionResult DeleteIngrediente(int idTarefa, int idIngrediente)
        {
            if (_context.TarefaIngrediente.Any(ti => ti.IngredienteId == idIngrediente && ti.TarefaId == idTarefa))
            {
                TarefaIngrediente tarefaIngrediente = new TarefaIngrediente(idTarefa, idIngrediente);
                _context.TarefaIngrediente.Remove(tarefaIngrediente);
                _context.SaveChanges();
                return Ok(tarefaIngrediente);
            }
            return NotFound();
        }

        // DELETE api/tarefa/tecnica
        [HttpDelete("tecnica/{idTarefa}/{idtecnica}")]
        public IActionResult Deletetecnica(int idTarefa, int idTecnica)
        {
            if (_context.TarefaTecnica.Any(ti => ti.TecnicaId == idTecnica && ti.TarefaId == idTarefa))
            {
                TarefaTecnica tarefaTecnica = new TarefaTecnica(idTarefa, idTecnica);
                _context.TarefaTecnica.Remove(tarefaTecnica);
                _context.SaveChanges();
                return Ok(tarefaTecnica);
            }
            return NotFound();
        }

        // DELETE api/tarefa/utensilio
        [HttpDelete("utensilio/{idTarefa}/{idutensilio}")]
        public IActionResult Deleteutensilio(int idTarefa, int idUtensilio)
        {
            if (_context.TarefaUtensilio.Any(ti => ti.UtensilioId == idUtensilio && ti.TarefaId == idTarefa))
            {
                TarefaUtensilio tarefaUtensilio = new TarefaUtensilio(idTarefa, idUtensilio);
                _context.TarefaUtensilio.Remove(tarefaUtensilio);
                _context.SaveChanges();
                return Ok(tarefaUtensilio);
            }
            return NotFound();
        }
    }
}
