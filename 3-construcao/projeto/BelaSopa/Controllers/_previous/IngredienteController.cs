using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Assistente;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BelaSopa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredienteController : Controller
    {
        private readonly BelaSopaContext _context;

        public IngredienteController(BelaSopaContext context)
        {
            _context = context;
        }

        // GET api/ingrediente
        [HttpGet]
        public ActionResult Get()
        {
            Ingrediente[] ingrs = _context.Ingrediente.ToArray<Ingrediente>();
            return Ok(ingrs);
        }

        // GET api/ingrediente/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            Ingrediente ingr = _context.Ingrediente.Find(id);
            if (ingr == null)
                return NotFound();

            return Ok(ingr);

        }

        // POST api/ingrediente
        [HttpPost]
        public IActionResult Post([FromBody] Ingrediente ingr)
        {
            _context.Ingrediente.Add(ingr);
            _context.SaveChanges();
            return new CreatedResult($"/api/Ingrediente/{ingr.IngredienteId}", ingr);
        }

        // PUT api/ingrediente/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Ingrediente ingr)
        {
            Ingrediente toUpdate = _context.Ingrediente.Find(id);
            if (toUpdate == null)
                return NotFound();

            toUpdate.ImagePath = ingr.ImagePath;
            toUpdate.Link = ingr.Link;
            toUpdate.Nome = ingr.Nome;
            toUpdate.Descricao = ingr.Descricao;

            _context.SaveChanges();

            return Ok(ingr);
        }

        // DELETE api/ingrediente/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Ingrediente a = _context.Ingrediente.Find(id);
            if (a == null)
            {
                return NotFound();
            }

            _context.Ingrediente.Remove(a);
            _context.SaveChanges();
            return NoContent();
        }



        public IActionResult Index()
        {
            return View();
        }
    }
}
