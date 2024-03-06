using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

using SimpleEntry.Models;

namespace SimpleEntry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly SimpleEntryContext _dbContext;

        public UsuarioController(SimpleEntryContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("AllUsuarios")]
        public IActionResult AllUsuarios()
        {
            try
            {
                List<Usuario> listaUsuarios = new List<Usuario>();
                listaUsuarios = _dbContext.Usuarios.ToList();

                return StatusCode(StatusCodes.Status200OK, new { message = "Lista devuelta", response = listaUsuarios });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet]
        [Route("Usuario/{usuarioId:int}")]
        public IActionResult Usuario(int usuarioId)
        {
            Usuario? oUsuario = new();

            try
            {
                oUsuario = _dbContext.Usuarios.Find(usuarioId);

                if (oUsuario == null)
                {
                    return BadRequest("No se encontró al usuario");
                }

                oUsuario = _dbContext.Usuarios.Where(us => us.Id == usuarioId).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { message = "Usuario encontrado", response = oUsuario });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        //[HttpPost]
        //[Route("CreateUsuario")]
        //public IActionResult CreateUsuario(Usuario usuario)
        //{
        //    try
        //    {
        //        string salt = BCrypt.Net.BCrypt.GenerateSalt();
        //        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(usuario.Pass, salt);

        //        usuario.Pass = hashedPassword;
        //        usuario.Salt = salt;


        //        _dbContext.Add(usuario);
        //        _dbContext.SaveChanges();

        //        return StatusCode(StatusCodes.Status201Created, new { message = "Usuario creado correctamente" });

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);

        //    }
        //}
    }
}
