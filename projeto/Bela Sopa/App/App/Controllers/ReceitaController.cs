using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using App.Models.Assistente;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceitaController : Controller
    {

        private readonly BelaSopaContext _context;

        public ReceitaController(BelaSopaContext context) {
            _context = context;
        }
        //GET api/receita
        [HttpGet]
        public Receita[] Get() {
            return _context.Receita.ToArray<Receita>();
        }

        //GET api/receita/1
        [HttpGet("{idReceita}")]
        public IActionResult Get(int idReceita) {
            Receita receita = _context.Receita.Find(idReceita);
            if (receita == null)
                return NotFound();
            return Ok(receita);
        }

        // GET api/receita/processo/1
        [HttpGet("processo/{idReceita}")]
        public IActionResult GetProcessos(int idReceita) {
            var receita = _context.Receita.Find(idReceita);
            if (receita == null)
                return NotFound();
            var processos = from processo in _context.Processo
                             join ReceitaProcesso in _context.ReceitaProcesso.Where(rt => rt.ReceitaId == idReceita)
                                  on processo.ProcessoId equals ReceitaProcesso.ProcessoId
                             select new {
                                 processo.ProcessoId,
                                 processo.Tempo
                             };
            //processos = processos.ToList<Processo>;
            return Ok(processos);
        }

        // GET api/receita/ingrediente/1
        [HttpGet("ingrediente/{idReceita}")]
        public IActionResult GetIngredientes(int idReceita) {
            var receita = _context.Receita.Find(idReceita);
            if (receita == null)
                return NotFound();
            var ingredientes = from ingrediente in _context.Ingrediente
                               join receitaIngrediente in _context.ReceitaIngrediente.Where(rt => rt.ReceitaId == idReceita)
                                    on ingrediente.IngredienteId equals receitaIngrediente.IngredienteId
                               select ingrediente;
            Ingrediente[] ingrsArray = ingredientes.ToArray<Ingrediente>();
            return Ok(ingrsArray);
        }


        // POST api/receita
        [HttpPost]
        public IActionResult Post([FromBody] Receita receita) {
            _context.Receita.Add(receita);
            _context.SaveChanges();
            return Ok(receita);
        }

        // POST api/receita/processo/1/2
        [HttpPost("processo/{idReceita}/{idProcesso}")]
        public IActionResult PostProcesso(int idReceita, int idProcesso) {
            bool receitaProcesso = _context.ReceitaProcesso.Any(tt => tt.ReceitaId == idReceita
                                                       && tt.ProcessoId == idProcesso);
            Receita receita = _context.Receita.Find(idReceita);
            Processo processo = _context.Processo.Find(idProcesso);
            if (receitaProcesso || receita == null || processo == null) {
                return NotFound();
            }
            ReceitaProcesso rt = new ReceitaProcesso(idReceita, idProcesso);
            _context.ReceitaProcesso.Add(rt);
            receita.Tempo += processo.Tempo;
            _context.SaveChanges();
            return Ok(rt);
        }

        // POST api/receita/ingrediente/
        [HttpPost("ingrediente/")]
        public IActionResult PostIngrediente([FromBody] ReceitaIngrediente ri) {
            bool receitaProcesso = _context.ReceitaIngrediente.Any(tt => tt.ReceitaId == ri.ReceitaId
                                                       && tt.IngredienteId == ri.IngredienteId);
            bool receita = _context.Receita.Any(r => r.ReceitaId == ri.ReceitaId);
            bool ingrediente = _context.Ingrediente.Any(i => i.IngredienteId == ri.IngredienteId);
            if (receitaProcesso || !receita|| !ingrediente) {
                return NotFound();
            }
            _context.ReceitaIngrediente.Add(ri);
            _context.SaveChanges();
            return Ok(ri);
        }



        //DELETE api/receita/1
        [HttpDelete("{idReceita}")]
        public IActionResult Delete(int idReceita) {
            Receita receita = _context.Receita.Find(idReceita);
            if (receita == null) return NotFound();

            _context.Receita.Remove(receita);
            _context.Remove(receita);
            return Ok(receita);
        }

        // DELETE api/processo/1/2
        [HttpDelete("processo/{idReceita}/{idProcesso}")]
        public IActionResult DeleteTarefa(int idReceita, int idProcesso) {
            ReceitaProcesso receitaProcesso = new ReceitaProcesso(idReceita, idProcesso);
            _context.ReceitaProcesso.Remove(receitaProcesso);

            Processo processo = _context.Processo.Find(idProcesso);
            Receita receita = _context.Receita.Find(idReceita);
            //Pelo invariante de inserção nunca deve ser nulo, mas por prevenção o "if" foi posto
            if (processo != null && receita != null)
                receita.Tempo -= processo.Tempo;

            _context.SaveChanges();
            return Ok();
        }

        // DELETE api/processo/1/2
        [HttpDelete("ingrediente/{idReceita}/{idIngrediente}")]
        public IActionResult DeleteIngrediente(int idReceita, int idIngrediente) {
            ReceitaIngrediente receitaProcesso = new ReceitaIngrediente(idReceita, idIngrediente);
            _context.ReceitaIngrediente.Remove(receitaProcesso);
            _context.SaveChanges();
            return Ok();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}