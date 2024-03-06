using Microsoft.AspNetCore.Components.Routing;
using Microsoft.EntityFrameworkCore;
using SimpleEntry.Models;
using SimpleEntry.Models.Request;
using SimpleEntry.Services;

namespace SimpleEntry.Rules
{
    public class AuthRule
    {
        private readonly SimpleEntryContext _dbContext;
        private readonly RegistroAccionRepository _registroAccionRepository;

        public AuthRule(SimpleEntryContext _context, RegistroAccionRepository _registroAcciones)
        {
            _dbContext = _context;
            _registroAccionRepository = _registroAcciones;
        }

        public bool MailExist(string? userMail)
        {
            Usuario? oUser = _dbContext.Usuarios.FirstOrDefault(user => user.Mail == userMail);

            if (oUser == null)
                return false;

            return true;
        }

        public bool CreateUser(CreateUserRequest usuario)
        {
            try
            {
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(usuario.Pass, salt);

                Usuario oUser = new()
                {
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Mail = usuario.Mail,
                    Pass = hashedPassword,
                    Salt = salt
                };

                _dbContext.Add(oUser);
                _dbContext.SaveChanges();


                oUser.Id = _dbContext.Usuarios.FirstOrDefault(user => user.Mail == usuario.Mail)!.Id;

                _registroAccionRepository.AddRegistroAccion(oUser.Id, "SignIn");


                return true;
            }
            catch
            {
                return false;
            }
        }

        public string ValidateUser(ValidateUserRequest usuario)
        {
            Usuario? oUser = _dbContext.Usuarios.FirstOrDefault(user => user.Mail == usuario.Mail);

            if (oUser == null)
            {
                return "The mail doesn't exist";
            }

            bool isPasswordOK = BCrypt.Net.BCrypt.Verify(usuario.Password, oUser.Pass);

            if (!isPasswordOK)
            {
                string checkPass = checkAttemps(oUser);
                _registroAccionRepository.AddRegistroAccion(oUser.Id, "LogIn Attempt");
                return checkPass;

            } else
            {
                oUser.Intentos_Login = 0;

                _dbContext.Update(oUser);
                _dbContext.SaveChanges();
            }

            

            return "JSON WEB TOKEN";
        }

        private string checkAttemps(Usuario usuario)
        {
            usuario.Intentos_Login++;

            if (usuario.Intentos_Login >= 3)
            {
                usuario.Intentos_Login = 0;

                _dbContext.Update(usuario);
                _dbContext.SaveChanges();

                return "Your account was blocked";

            }

            _dbContext.Update(usuario);
            _dbContext.SaveChanges();

            return $"Wrong password. Attempts remaining: " + (3 - usuario.Intentos_Login);
        }
    }
}
