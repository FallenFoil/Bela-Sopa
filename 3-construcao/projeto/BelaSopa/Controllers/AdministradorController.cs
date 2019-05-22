using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BelaSopa.Models;
using BelaSopa.Models.Utilizadores;
using Microsoft.AspNetCore.Mvc;

namespace BelaSopa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministradorController : Controller
    {
        private readonly BelaSopaDbContext _context;

        public AdministradorController(BelaSopaDbContext context) {
            _context = context;
        }


        // GET api/administrador
        [HttpGet]
        public ActionResult Get() {
            Administrador[] admins = _context.Administrador.ToArray<Administrador>();
            return Ok(admins);
        }

        // GET api/administrador/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id) {
            Administrador admin = _context.Administrador.Find(id);
            if (admin == null)
                return NotFound();

            return Ok(admin);

        }

        // POST api/administrador
        [HttpPost]
        public IActionResult Post([FromBody] Administrador a) {
            _context.Administrador.Add(a);
            _context.SaveChanges();
            //return new CreatedResult($"/api/administrador/{a.UtilizadorId}", a);
            return new OkResult();
        }

        // PUT api/administrador/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Administrador a) {
            Administrador toUpdate = _context.Administrador.Find(id);
            if (toUpdate == null)
                return NotFound();

            //toUpdate.Distrito = a.Distrito;
            //toUpdate.Email = a.Email;
            //toUpdate.Nome = a.Nome;

            _context.SaveChanges();

            return Ok(a);
        }

        // DELETE api/administrador/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            Administrador a = _context.Administrador.Find(id);
            if (a == null) {
                return NotFound();
            }

            _context.Administrador.Remove(a);
            _context.SaveChanges();
            return NoContent();
        }
        public IActionResult Index() {

            return View();
        }
    }
}
