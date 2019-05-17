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

        // POST api/receita
        [HttpPost]
        public IActionResult Post([FromBody] Receita receita) {
            _context.Receita.Add(receita);
            _context.SaveChanges();
            return Ok(receita);
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

        public IActionResult Index()
        {
            return View();
        }
    }
}