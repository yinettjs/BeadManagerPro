using BeadManagerPro.Domain.Entities.Encargos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeadManagerPro.Application.Dtos.Encargos;

public class MaterialDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int StockActual { get; set; }
}
public class CreateMaterialDto
{
    public string Nombre { get; set; } = string.Empty;
    public int Stock { get; set; }
    public decimal PrecioUnitario { get; set; }
}