using Microsoft.EntityFrameworkCore;
using SimpleEntry.Models;

namespace SimpleEntry.Services
{

    public class RegistroAccionRepository
    {
        private readonly SimpleEntryContext _dbContext;

        public RegistroAccionRepository(SimpleEntryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddRegistroAccion(int usuarioId, string accion)
        {
            RegistroAcciones registroAccion = new()
            {
                UsuarioId = usuarioId,
                Accion = accion,
                FechaHora = DateTime.Now
            };

            _dbContext.RegistroAcciones.Add(registroAccion);
            _dbContext.SaveChanges();
        }
    }
}
