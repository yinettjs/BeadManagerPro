using BeadManagerPro.Domain.Entities.Encargos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeadManagerPro.Domain.Entities.Usuarios;

public class Artesana
{
    // 1. AGREGA ESTO: Es la llave primaria que EF necesita
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string? Especialidad { get; set; }
    public bool Activa { get; set; } = true;
    public bool IsDeleted { get; set; }

    public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
