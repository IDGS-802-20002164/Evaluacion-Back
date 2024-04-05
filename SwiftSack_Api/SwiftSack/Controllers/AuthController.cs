using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwiftSack.Context;
using SwiftSack.Models;

namespace SwiftSack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Usuario model)
        {
            try
            {
                var usuario = new Usuario
                {
                    name = model.name,
                    email = model.email,
                    password = model.password,
                    telefono = model.telefono,
                    active = model.active,
                    confirmed_at = model.confirmed_at,
                    roleId = model.roleId // Asignar el ID del rol "cliente"
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync(); // Guardar el usuario

                if (usuario.Id > 0)
                {
                    return Ok(new { Message = "Registro exitoso" });
                }

                return BadRequest(new { Message = "No se pudo crear el usuario" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLogin model)
        {
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.email == model.email && u.password == model.password);

                if (usuario != null)
                {
                    // Lógica de inicio de sesión y autenticación exitosa
                    return Ok(usuario);
                }

                return Unauthorized(new { Message = "Credenciales inválidas" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            try
            {
                // Buscar el usuario por su ID
                var user = await _context.Usuarios.FindAsync(id);

                if (user == null)
                {
                    return NotFound(new { Message = "Usuario no encontrado" });
                }

                // Remover los campos que no deben ser modificados
                user.active = false; // Ignorar el campo "active"
                user.confirmed_at = null; // Ignorar el campo "confirmed_at"

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("profile/{id}")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] Usuario updatedUser)
        {
            try
            {
                // Buscar el usuario por su ID
                var user = await _context.Usuarios.FindAsync(id);

                if (user == null)
                {
                    return NotFound(new { Message = "Usuario no encontrado" });
                }

                // Actualizar los campos permitidos
                user.name = updatedUser.name;
                user.email = updatedUser.email;
                user.password = updatedUser.password;
                user.telefono = updatedUser.telefono;


                await _context.SaveChangesAsync();

                return Ok(new { Message = "Perfil actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException != null ? ex.InnerException.Message : "N/A";

                return BadRequest(new { Message = ex.Message, InnerException = innerException });
            }
        }
    }
}
