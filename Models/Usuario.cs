using System;
using System.Collections.Generic;

namespace SimpleEntry.Models;

public partial class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Mail { get; set; } = "";
    public string Pass { get; set; } = string.Empty;
    public DateTime FechaHora { get; set; }
    public string Salt { get; set; } = string.Empty;
    public int Intentos_Login { get; set; } = 0;
    public virtual ICollection<RegistroAcciones>? OAcciones { get; set; } = new List<RegistroAcciones>();
}
