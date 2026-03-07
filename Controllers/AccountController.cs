using DoneTask.Models;
using DoneTask.Models.viewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoneTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public AccountController(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegistroUsuario model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new Usuario
            {
                UserName = model.Username,
                Email = model.Email,
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Rol por defecto
            await _userManager.AddToRoleAsync(user, "COLABORADOR");

            return Ok();
        }

        // ============================
        // LOGIN
        // ============================
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] InicioSesion model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return Unauthorized("Credenciales inválidas");

            var result = await _signInManager.PasswordSignInAsync(
                user,
                model.Password,
                isPersistent: false,
                lockoutOnFailure: false
            );

            if (!result.Succeeded)
                return Unauthorized("Credenciales inválidas");

            
            return Ok();
        }



        [HttpPost("logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logout exitoso");
        }

        [HttpGet("me")]
        public IActionResult Me()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            return Ok(new
            {
                email = User.Identity.Name
            });
        }
    }
}

