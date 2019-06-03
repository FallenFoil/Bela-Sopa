using BelaSopa.Models;
using BelaSopa.Models.DomainModels.Assistente;
using BelaSopa.Models.DomainModels.Utilizadores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BelaSopa.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class ClienteController : Controller
    //{

    //    private readonly BelaSopaContext _context;

    //    public ClienteController(BelaSopaContext context)
    //    {
    //        _context = context;
    //    }

    //    // GET api/cliente
    //    [HttpGet]
    //    public ActionResult Get()
    //    {
    //        Cliente[] clientes = _context.Clientes.ToArray<Cliente>();
    //        return Ok(clientes);
    //    }

    //    // GET api/cliente/5
    //    [HttpGet("{id}")]
    //    public ActionResult<string> Get(int id)
    //    {
    //        Cliente c = _context.Clientes.Find(id);
    //        if (c == null)
    //            return NotFound();

    //        return Ok(c);
    //    }

    //    // GET api/cliente/finalizado
    //    [HttpGet("finalizado/{idCliente}")]
    //    public IActionResult GetReceitasFinalizadas(int idCliente)
    //    {
    //        var cliente = _context.Clientes.Find(idCliente);
    //        if (cliente == null)
    //            return NotFound();
    //        Receita[] receitas = getReceitaOfCliente<ClienteFinalizado>(idCliente, _context.ClientesFinalizado);
    //        return Ok(receitas);
    //    }

    //    // GET api/cliente/ementasemanal/1
    //    [HttpGet("ementasemanal/{idCliente}")]
    //    public IActionResult GetReceitasEmentaSemanal(int idCliente)
    //    {
    //        var cliente = _context.Clientes.Find(idCliente);
    //        if (cliente == null)
    //            return NotFound();
    //        Receita[] receitas = getReceitaOfCliente<ClienteEmentaSemanal>(idCliente, _context.ClientesEmentaSemanal);
    //        return Ok(receitas);
    //    }

    //    // GET api/cliente/favorito/1
    //    [HttpGet("favorito/{idCliente}")]
    //    public IActionResult GetReceitasFavoritas(int idCliente)
    //    {
    //        var cliente = _context.Clientes.Find(idCliente);
    //        if (cliente == null)
    //            return NotFound();
    //        Receita[] receitas = getReceitaOfCliente<ClienteFavorito>(idCliente, _context.ClientesFavorito);
    //        return Ok(receitas);
    //    }

    //    private Receita[] getReceitaOfCliente<T>(int idCliente, DbSet<T> clienteReceitaDb) where T : ClienteReceita
    //    {

    //        var receitas = from receita in _context.Receita
    //                       join clienteReceita in clienteReceitaDb.Where(rt => rt.ClienteId == idCliente)
    //                            on receita.ReceitaId equals clienteReceita.ReceitaId
    //                       select receita;
    //        Receita[] receitasArray = receitas.ToArray<Receita>();
    //        return receitasArray;
    //    }

    //    // POST api/cliente
    //    [HttpPost]
    //    public bool Post([FromBody] Cliente c)
    //    {
    //        //c.Password = Encript.HashPassword(c.Password);
    //        _context.Clientes.Add(c);
    //        _context.SaveChanges();
    //        return true;
    //        // return new CreatedResult($"/api/cliente/{c.UtilizadorId}", c);
    //    }

    //    // PUT api/cliente/5
    //    [HttpPut("{id}")]
    //    public IActionResult Put(int id, [FromBody] Cliente c)
    //    {
    //        Cliente toUpdate = _context.Clientes.Find(id);
    //        if (toUpdate == null)
    //            return NotFound();

    //        //toUpdate.Distrito = c.Distrito;
    //        //toUpdate.Email = c.Email;
    //        //toUpdate.Localização = c.Localização;
    //        //toUpdate.Nome = c.Nome;

    //        _context.SaveChanges();

    //        return Ok(c);
    //    }


    //    // POST api/cliente/finalizado
    //    [HttpPost("finalizado")]
    //    public IActionResult PostFinalizado([FromBody] ClienteFinalizado cf)
    //    {
    //        bool added = addReceitaToCliente<ClienteFinalizado>(cf, _context.ClientesFinalizado);
    //        if (!added) return NotFound();
    //        return Ok(cf);
    //    }


    //    // POST api/cliente/ementasemanal
    //    [HttpPost("ementasemanal")]
    //    public IActionResult PostEmentaSemanal([FromBody] ClienteEmentaSemanal ce)
    //    {
    //        bool added = addReceitaToCliente<ClienteEmentaSemanal>(ce, _context.ClientesEmentaSemanal);
    //        if (!added) return NotFound();
    //        return Ok(ce);
    //    }

    //    // POST api/cliente/favorito
    //    [HttpPost("favorito")]
    //    public IActionResult PostFavorito([FromBody] ClienteFavorito cf)
    //    {
    //        bool added = addReceitaToCliente<ClienteFavorito>(cf, _context.ClientesFavorito);
    //        if (!added) return NotFound();
    //        return Ok(cf);
    //    }


    //    private bool addReceitaToCliente<T>(T toAdd, DbSet<T> clienteReceitaDb) where T : ClienteReceita
    //    {
    //        bool exists = clienteReceitaDb.Any(c => c.ReceitaId == toAdd.ReceitaId && c.ClienteId == toAdd.ClienteId);
    //        bool cliente = _context.Clientes.Any(c => c.Id == toAdd.ClienteId);
    //        bool receita = _context.Receita.Any(r => r.ReceitaId == toAdd.ReceitaId);
    //        if (exists || !cliente || !receita) return false;

    //        clienteReceitaDb.Add(toAdd);
    //        _context.SaveChanges();
    //        return true;
    //    }

    //    // DELETE api/cliente/5
    //    [HttpDelete("{id}")]
    //    public IActionResult Delete(int id)
    //    {
    //        Cliente c = _context.Clientes.Find(id);
    //        if (c == null)
    //        {
    //            return NotFound();
    //        }

    //        _context.Clientes.Remove(c);
    //        _context.SaveChanges();
    //        return NoContent();
    //    }

    //    // DELETE api/cliente/finalizado/1/2
    //    [HttpDelete("finalizado/{idCliente}/{idReceita}")]
    //    public IActionResult DeleteFinalizado(int idCliente, int idReceita)
    //    {
    //        ClienteFinalizado cf = new ClienteFinalizado(idCliente, idReceita);
    //        _context.ClientesFinalizado.Remove(cf);
    //        _context.SaveChanges();
    //        return Ok(cf);
    //    }
    //    // DELETE api/cliente/finalizado/1/2
    //    [HttpDelete("favorito/{idCliente}/{idReceita}")]
    //    public IActionResult DeleteFavorito(int idCliente, int idReceita)
    //    {
    //        ClienteFavorito cf = new ClienteFavorito(idCliente, idReceita);
    //        _context.ClientesFavorito.Remove(cf);
    //        _context.SaveChanges();
    //        return Ok(cf);
    //    }
    //    // DELETE api/cliente/finalizado/1/2
    //    [HttpDelete("ementasemanal/{idCliente}/{idReceita}")]
    //    public IActionResult DeleteEmentaSemanal(int idCliente, int idReceita)
    //    {
    //        ClienteEmentaSemanal ces = new ClienteEmentaSemanal(idCliente, idReceita);
    //        _context.ClientesEmentaSemanal.Remove(ces);
    //        _context.SaveChanges();
    //        return Ok(ces);
    //    }

    //    public IActionResult Index()
    //    {

    //        return View();
    //    }
    //}
}
