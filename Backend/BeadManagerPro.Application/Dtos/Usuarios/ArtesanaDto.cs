using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeadManagerPro.Application.Dtos.Usuarios;
public class ArtesanaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string? Especialidad { get; set; }
    public bool Activa { get; set; }
    public int EncargosActivos { get; set; }
}

public class CreateArtesanaDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string? Especialidad { get; set; }
}