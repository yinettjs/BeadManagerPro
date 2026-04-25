using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeadManagerPro.Domain.Entities.Encargos;

public class Pieza
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal PrecioVenta { get; set; }
    public int TiempoEstimadoMinutos { get; set; } 
    public bool IsDeleted { get; set; }
}
