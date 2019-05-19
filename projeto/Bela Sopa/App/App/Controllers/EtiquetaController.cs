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
    public class EtiquetaController : Controller
    {
        private readonly BelaSopaContext _context;

        public EtiquetaController(BelaSopaContext context) {
            _context = context;
        }

        // GET api/etiqueta
        [HttpGet]
        public Etiqueta[] Get() {
            Etiqueta[] etiquetas = _context.Etiqueta.ToArray<Etiqueta>();
            return etiquetas;
        }

        // DELETE api/etiqueta/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            Etiqueta e = _context.Etiqueta.Find(id);
            if (e == null)
                return NotFound();
            _context.Etiqueta.Remove(e);
            _context.SaveChanges();
            return Ok(e);
        }

        // POST api/etiqueta
        [HttpPost]
        public IActionResult Post([FromBody] Etiqueta e) {
            _context.Etiqueta.Add(e);
            _context.SaveChanges();
            return Ok(e);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
