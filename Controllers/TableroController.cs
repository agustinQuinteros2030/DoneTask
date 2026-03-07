using DoneTask.Data;
using DoneTask.Models;
using DoneTask.Models.DoneTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Security.Claims;
using System.Linq;


namespace DoneTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TableroController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ApplicationDbContext _context;

        public TableroController(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            ApplicationDbContext context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        [HttpGet("mis-tableros")]
        public async Task<IActionResult> MisTableros()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var guidUserId = Guid.Parse(userId);

            var tableros = await _context.Tableros
                .Include(t => t.Creador)
                .Include(t => t.UsuariosTablero)
                    .ThenInclude(ut => ut.Usuario)
                .Where(t =>
                    t.CreadorId == guidUserId ||
                    t.UsuariosTablero.Any(ut => ut.UsuarioId == guidUserId))
                .ToListAsync();

            return Ok(tableros);
        }


        [HttpGet("tablero/{tableroId}")]
        public async Task<IActionResult> ObtenerListas(Guid tableroId)
        {
            var listas = await _context.ListasTareas
                .Where(l => l.TableroId == tableroId)
                .ToListAsync();

            return Ok(listas);
        }



        // GET: api/tablero/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTablero(Guid id)
        {
            var tablero = await _context.Tableros
       .Include(t => t.Creador)
       .Include(t => t.ListaTareas)
       .FirstOrDefaultAsync(t => t.Id == id);


            if (tablero == null)
                return NotFound();

            return Ok(tablero);
        }

        // POST: api/tablero
        [HttpPost]
        public async Task<IActionResult> CrearTablero([FromBody] Tablero tablero)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            tablero.CreadorId = Guid.Parse(userId);

            _context.Tableros.Add(tablero);

            await _context.SaveChangesAsync();

            return Ok(tablero);
        }
        // PUT: api/tablero/{id}

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, [FromBody] Tablero model)
        {
            var tablero = await _context.Tableros.FindAsync(id);

            if (tablero == null)
                return NotFound();

            tablero.Nombre = model.Nombre;
            tablero.Descripcion = model.Descripcion;

            await _context.SaveChangesAsync();

            return Ok(tablero);
        }

        // DELETE: api/tablero/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarTablero(Guid id)
        {
            var tablero = await _context.Tableros.FindAsync(id);

            if (tablero == null)
                return NotFound();

            _context.Tableros.Remove(tablero);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}/usuarios")]
        public async Task<IActionResult> GetUsuariosTablero(Guid id)
        {
            var tablero = await _context.Tableros
                .Include(t => t.UsuariosTablero)
                .ThenInclude(ut => ut.Usuario)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tablero == null)
                return NotFound();

            return Ok(tablero.UsuariosTablero);
        }


        

    }





}
