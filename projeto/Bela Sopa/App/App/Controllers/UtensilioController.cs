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
    public class UtensilioController : Controller {
        private readonly BelaSopaDbContext _context;

        public UtensilioController(BelaSopaDbContext context) {
            _context = context;
        }

        // GET api/utensilio
        [HttpGet]
        public ActionResult Get() {
            Utensilio[] utens = _context.Utensilio.ToArray<Utensilio>();
            return Ok(utens);
        }

        // GET api/utensilio/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id) {
            Utensilio uten = _context.Utensilio.Find(id);
            if (uten == null)
                return NotFound();

            return Ok(uten);

        }

        // POST api/utensilio
        [HttpPost]
        public IActionResult Post([FromBody] Utensilio uten) {
            _context.Utensilio.Add(uten);
            _context.SaveChanges();
            return new CreatedResult($"/api/Utensilio/{uten.UtensilioId}", uten);
        }

        // PUT api/utensilio/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Utensilio uten) {
            Utensilio toUpdate = _context.Utensilio.Find(id);
            if (toUpdate == null)
                return NotFound();

            toUpdate.ImagePath = uten.ImagePath;
            toUpdate.Link = uten.Link;
            toUpdate.Nome = uten.Nome;
            toUpdate.Descricao = uten.Descricao;

            _context.SaveChanges();

            return Ok(uten);
        }

        // DELETE api/utensilio/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            Utensilio a = _context.Utensilio.Find(id);
            if (a == null) {
                return NotFound();
            }

            _context.Utensilio.Remove(a);
            _context.SaveChanges();
            return NoContent();
        }

        public IActionResult Index() {
            return View();
        }
    }
}