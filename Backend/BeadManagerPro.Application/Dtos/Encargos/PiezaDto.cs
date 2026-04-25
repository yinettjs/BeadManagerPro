using BeadManagerPro.Domain.Entities.Encargos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeadManagerPro.Application.Dtos.Encargos;

public class PiezaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
}
public class CreatePiezaDto
{
    public string Nombre { get; set; } = string.Empty;
    public decimal PrecioVenta { get; set; }
    public string? Descripcion { get; set; }
}