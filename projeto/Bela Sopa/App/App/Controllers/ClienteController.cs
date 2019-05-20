using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BelaSopa.Models;
using BelaSopa.Models.Assistente;
using BelaSopa.Models.Utilizadores;
using BelaSopa.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BelaSopa.Controllers {
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

        // GET api/cliente/finalizado
        [HttpGet("finalizado/{idCliente}")]
        public IActionResult GetReceitasFinalizadas(int idCliente) {
            var cliente = _context.Cliente.Find(idCliente);
            if (cliente == null)
                return NotFound();
            Receita[] receitas = getReceitaOfCliente<ClienteFinalizado>(idCliente, _context.ClienteFinalizado);
            return Ok(receitas);
        }

        // GET api/cliente/ementasemanal/1
        [HttpGet("ementasemanal/{idCliente}")]
        public IActionResult GetReceitasEmentaSemanal(int idCliente) {
            var cliente = _context.Cliente.Find(idCliente);
            if (cliente == null)
                return NotFound();
            Receita[] receitas = getReceitaOfCliente<ClienteEmentaSemanal>(idCliente, _context.ClienteEmentaSemanal);
            return Ok(receitas);
        }

        // GET api/cliente/favorito/1
        [HttpGet("favorito/{idCliente}")]
        public IActionResult GetReceitasFavoritas(int idCliente) {
            var cliente = _context.Cliente.Find(idCliente);
            if (cliente == null)
                return NotFound();
            Receita[] receitas = getReceitaOfCliente<ClienteFavorito>(idCliente, _context.ClienteFavorito);
            return Ok(receitas);
        }

        private Receita[] getReceitaOfCliente<T>(int idCliente, DbSet<T> clienteReceitaDb) where T : ClienteReceita {
            
            var receitas = from receita in _context.Receita
                               join clienteReceita in clienteReceitaDb.Where(rt => rt.ClienteId == idCliente)
                                    on receita.ReceitaId equals clienteReceita.ReceitaId
                               select receita;
            Receita[] receitasArray = receitas.ToArray<Receita>();
            return receitasArray;
        }

        // POST api/cliente
        [HttpPost]
        public bool Post([FromBody] Cliente c) {
            c.Password = Encript.HashPassword(c.Password);
            _context.Cliente.Add(c);
            _context.SaveChanges();
            return true;
           // return new CreatedResult($"/api/cliente/{c.UtilizadorId}", c);
        }

        // PUT api/cliente/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Cliente c) {
            Cliente toUpdate = _context.Cliente.Find(id);
            if (toUpdate == null)
                return NotFound();

            toUpdate.Distrito = c.Distrito;
            toUpdate.Email = c.Email;
            toUpdate.Localização = c.Localização;
            toUpdate.Nome = c.Nome;

            _context.SaveChanges(); 

            return Ok(c);
        }

        
        // POST api/cliente/finalizado
        [HttpPost("finalizado")]
        public IActionResult PostFinalizado([FromBody] ClienteFinalizado cf) {
            bool added = addReceitaToCliente<ClienteFinalizado>(cf, _context.ClienteFinalizado);
            if (!added) return NotFound();
            return Ok(cf);
        }


        // POST api/cliente/ementasemanal
        [HttpPost("ementasemanal")]
        public IActionResult PostEmentaSemanal([FromBody] ClienteEmentaSemanal ce) {
            bool added = addReceitaToCliente<ClienteEmentaSemanal>(ce, _context.ClienteEmentaSemanal);
            if (!added) return NotFound();
            return Ok(ce);
        }

        // POST api/cliente/favorito
        [HttpPost("favorito")]
        public IActionResult PostFavorito([FromBody] ClienteFavorito cf) {
            bool added = addReceitaToCliente<ClienteFavorito>(cf, _context.ClienteFavorito);
            if (!added) return NotFound();
            return Ok(cf);
        }


        private bool addReceitaToCliente<T>(T toAdd, DbSet<T> clienteReceitaDb) where T : ClienteReceita {
            bool exists = clienteReceitaDb.Any(c => c.ReceitaId == toAdd.ReceitaId && c.ClienteId == toAdd.ClienteId);
            bool cliente = _context.Cliente.Any(c => c.UtilizadorId == toAdd.ClienteId);
            bool receita = _context.Receita.Any(r => r.ReceitaId == toAdd.ReceitaId);
            if (exists || !cliente || !receita) return false;

            clienteReceitaDb.Add(toAdd);
            _context.SaveChanges();
            return true;
        }

        // DELETE api/cliente/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            Cliente c = _context.Cliente.Find(id);
            if (c == null) {
                return NotFound();
            }

            _context.Cliente.Remove(c);
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE api/cliente/finalizado/1/2
        [HttpDelete("finalizado/{idCliente}/{idReceita}")]
        public IActionResult DeleteFinalizado(int idCliente, int idReceita) {
            ClienteFinalizado cf = new ClienteFinalizado(idCliente, idReceita);
            _context.ClienteFinalizado.Remove(cf);
            _context.SaveChanges();
            return Ok(cf);
        }
        // DELETE api/cliente/finalizado/1/2
        [HttpDelete("favorito/{idCliente}/{idReceita}")]
        public IActionResult DeleteFavorito(int idCliente, int idReceita) {
            ClienteFavorito cf = new ClienteFavorito(idCliente, idReceita);
            _context.ClienteFavorito.Remove(cf);
            _context.SaveChanges();
            return Ok(cf);
        }
        // DELETE api/cliente/finalizado/1/2
        [HttpDelete("ementasemanal/{idCliente}/{idReceita}")]
        public IActionResult DeleteEmentaSemanal(int idCliente, int idReceita) {
            ClienteEmentaSemanal ces = new ClienteEmentaSemanal(idCliente, idReceita);
            _context.ClienteEmentaSemanal.Remove(ces);
            _context.SaveChanges();
            return Ok(ces);
        }

        public IActionResult Index()
        {
           
            return View();
        }
    }
}