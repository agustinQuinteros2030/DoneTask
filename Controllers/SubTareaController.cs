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
    public class SubTareaController : ControllerBase

    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ApplicationDbContext _context;

        public SubTareaController(
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
        public async Task<IActionResult> CrearSubtarea([FromBody] Subtarea subtarea)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tareaExiste = await _context.Tareas
                .AnyAsync(t => t.Id == subtarea.TareaId);

            if (!tareaExiste)
                return NotFound("La tarea no existe");

            subtarea.Id = Guid.NewGuid();
            subtarea.FechaCreacion = DateTime.UtcNow;

            _context.Subtareas.Add(subtarea);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerSubtarea), new { id = subtarea.Id }, subtarea);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerSubtarea(Guid id)
        {
            var subtarea = await _context.Subtareas
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (subtarea == null)
                return NotFound();

            return Ok(subtarea);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarSubtarea(Guid id, [FromBody] Subtarea model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var subtarea = await _context.Subtareas
                .FirstOrDefaultAsync(s => s.Id == id);

            if (subtarea == null)
                return NotFound("Subtarea no encontrada");

            subtarea.Nombre = model.Nombre;
            subtarea.Descripcion = model.Descripcion;
            subtarea.Completada = model.Completada;

            await _context.SaveChangesAsync();

            return Ok(subtarea);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarSubtarea(Guid id)
        {
            var subtarea = await _context.Subtareas
                .FirstOrDefaultAsync(s => s.Id == id);

            if (subtarea == null)
                return NotFound("Subtarea no encontrada");

            _context.Subtareas.Remove(subtarea);

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
