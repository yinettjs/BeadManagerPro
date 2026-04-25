using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeadManagerPro.Domain.Entities.Encargos;

public class Material
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty; 
    public int StockActual { get; set; }
    public decimal CostoPorUnidad { get; set; }
    public bool IsDeleted { get; set; }
}