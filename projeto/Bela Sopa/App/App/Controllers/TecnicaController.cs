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
    public class TecnicaController : Controller {
        private readonly BelaSopaContext _context;

        public TecnicaController(BelaSopaContext context) {
            _context = context;
        }

        // GET api/tecnica
        [HttpGet]
        public ActionResult Get() {
            Tecnica[] tecns = _context.Tecnica.ToArray<Tecnica>();
            return Ok(tecns);
        }

        // GET api/tecnica/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id) {
            Tecnica tecn = _context.Tecnica.Find(id);
            if (tecn == null)
                return NotFound();

            return Ok(tecn);

        }

        // POST api/tecnica
        [HttpPost]
        public IActionResult Post([FromBody] Tecnica tecn) {
            _context.Tecnica.Add(tecn);
            _context.SaveChanges();
            return new CreatedResult($"/api/Tecnica/{tecn.TecnicaId}", tecn);
        }

        // PUT api/tecnica/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Tecnica tecn) {
            Tecnica toUpdate = _context.Tecnica.Find(id);
            if (toUpdate == null)
                return NotFound();

            toUpdate.ImagePath = tecn.ImagePath;
            toUpdate.Link = tecn.Link;
            toUpdate.Nome = tecn.Nome;
            toUpdate.Descricao = tecn.Descricao;

            _context.SaveChanges();

            return Ok(tecn);
        }

        // DELETE api/tecnica/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            Tecnica a = _context.Tecnica.Find(id);
            if (a == null) {
                return NotFound();
            }

            _context.Tecnica.Remove(a);
            _context.SaveChanges();
            return NoContent();
        }

        public IActionResult Index() {
            return View();
        }
    }
}