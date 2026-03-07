using DoneTask.Data;
using DoneTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace DoneTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TareaController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ApplicationDbContext _context;

        public TareaController(
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
        public async Task<IActionResult> CrearTarea([FromBody] Tarea tarea)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var listaExiste = await _context.ListasTareas
                .AnyAsync(l => l.Id == tarea.ListaTareaId);

            if (!listaExiste)
                return NotFound("La lista no existe");

            tarea.Id = Guid.NewGuid();
            tarea.FechaCreacion = DateTime.UtcNow;

            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerTarea), new { id = tarea.Id }, tarea);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerTarea(Guid id)
        {
            var tarea = await _context.Tareas
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tarea == null)
                return NotFound();

            return Ok(tarea);
        }
        [HttpGet("lista/{listaId}")]
        public async Task<IActionResult> ObtenerTareasPorLista(Guid listaId)
        {
            var tareas = await _context.Tareas
                .Where(t => t.ListaTareaId == listaId)
                .AsNoTracking()
                .ToListAsync();

            return Ok(tareas);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarTarea(Guid id, [FromBody] Tarea model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tarea = await _context.Tareas
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tarea == null)
                return NotFound("Tarea no encontrada");

            tarea.Nombre = model.Nombre;
            tarea.Descripcion = model.Descripcion;
            tarea.Completada = model.Completada;

            await _context.SaveChangesAsync();

            return Ok(tarea);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarTarea(Guid id)
        {
            var tarea = await _context.Tareas
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tarea == null)
                return NotFound("Tarea no encontrada");

            _context.Tareas.Remove(tarea);

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
