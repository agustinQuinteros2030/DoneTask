using DoneTask.Data;
using DoneTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DoneTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ListaTareaController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ApplicationDbContext _context;

        public ListaTareaController(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            ApplicationDbContext context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> CrearLista([FromBody] ListaTarea lista)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tableroExiste = await _context.Tableros
                .AnyAsync(t => t.Id == lista.TableroId);

            if (!tableroExiste)
                return NotFound("El tablero no existe");

            lista.Id = Guid.NewGuid();
            lista.FechaCreacion = DateTime.UtcNow;

            _context.ListasTareas.Add(lista);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerLista), new { id = lista.Id }, lista);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarLista(Guid id, [FromBody] ListaTarea model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var lista = await _context.ListasTareas
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lista == null)
                return NotFound("Lista no encontrada");

            lista.Nombre = model.Nombre;
            lista.Descripcion = model.Descripcion;

            await _context.SaveChangesAsync();

            return Ok(lista);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarLista(Guid id)
        {
            var lista = await _context.ListasTareas
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lista == null)
                return NotFound("Lista no encontrada");

            _context.ListasTareas.Remove(lista);

            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerLista(Guid id)
        {
            var lista = await _context.ListasTareas
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lista == null)
                return NotFound();

            return Ok(lista);
        }
    }
}
