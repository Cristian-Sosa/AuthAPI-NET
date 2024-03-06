using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleEntry.Models;
using SimpleEntry.Models.Request;
using SimpleEntry.Rules;
using SimpleEntry.Services;

namespace SimpleEntry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SimpleEntryContext _dbContext;
        RegistroAccionRepository _registroAccionRepository;
        AuthRule _authRule;

        public AuthController(SimpleEntryContext _context, RegistroAccionRepository _registroAcciones, AuthRule authRule)
        {
            _dbContext = _context;
            _registroAccionRepository = _registroAcciones;
            _authRule = authRule;
        }

        [HttpGet]
        [Route("ValidateMail")]
        public IActionResult UsernameExist([FromQuery]string userMail)
        {
            try
            {
                bool mailExist = _authRule.MailExist(userMail);

                if (!mailExist)
                {
                    return StatusCode(StatusCodes.Status200OK, new { message = "The email doesn't exist" });
                }

                return StatusCode(StatusCodes.Status409Conflict, new { message = "The email already exists" });

            } catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = "An error occurred while validating the email" });

            }
        }

        [HttpPost]
        [Route("CreateUser")]
        public IActionResult CreateUsuario([FromBody]CreateUserRequest usuario)
        {
            bool mailExist = _authRule.MailExist(usuario.Mail);

            if (mailExist)
            {
                return StatusCode(StatusCodes.Status409Conflict, new { message = "The mail already exists" });
            }

            bool userCreated = _authRule.CreateUser(usuario);

            if (!userCreated)
            {
                return StatusCode(StatusCodes.Status409Conflict, new { message = "An error occurred while the user was creating" });
            }

            return StatusCode(StatusCodes.Status201Created, new { message = "User created" });
        }

        [HttpPost("ValidateUser")]
        public IActionResult ValidateUser(ValidateUserRequest usuario)
        {
            string response = _authRule.ValidateUser(usuario);

            return StatusCode(StatusCodes.Status200OK, new { message = response });
        }
    }
}
