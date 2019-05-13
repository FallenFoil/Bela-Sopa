using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using App.Models.Utilizadores;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {

        private readonly BelaSopaContext _context;

        public ClienteController(BelaSopaContext context) {
            _context = context;
        }

        // GET api/cliente
        [HttpGet]
        public ActionResult Get() {
            Cliente[] clientes = _context.Cliente.ToArray<Cliente>();
            return Ok(clientes);
        }

        // GET api/cliente/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id) {
            Cliente c = _context.Cliente.Find(id);
            if (c == null)
                return NotFound();
          
        return Ok(c);

        }

        // POST api/cliente
        [HttpPost]
        public void Post([FromBody] Cliente c) {
            _context.Cliente.Add(c);
            _context.SaveChanges();
           // return new CreatedResult($"/api/cliente/{c.UtilizadorId}", c);
        }

        // PUT api/cliente/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE api/cliente/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}