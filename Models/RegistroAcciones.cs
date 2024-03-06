using System;
using System.Collections.Generic;

namespace SimpleEntry.Models;

public partial class RegistroAcciones
{
    public int Id { get; set; }
    public int? UsuarioId { get; set; }
    public string? Accion { get; set; }
    public DateTime? FechaHora { get; set; }
    public virtual Usuario? Usuario { get; set; }
}
