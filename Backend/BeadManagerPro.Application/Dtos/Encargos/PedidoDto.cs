using BeadManagerPro.Domain.Entities.Encargos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeadManagerPro.Application.Dtos.Encargos;

public class PedidoDto
{
    public int Id { get; set; }
    public string Estado { get; set; } = string.Empty;
    public DateTime FechaEntrega { get; set; }
    public string NombreCliente { get; set; } = string.Empty;
    public string NombreArtesana { get; set; } = string.Empty;
    public decimal Total { get; set; }
}
public class CreatePedidoDto
{
    public int ClienteId { get; set; }
    public int ArtesanaId { get; set; } // Antes CostureraId
    public decimal PrecioTotal { get; set; }
    public DateTime FechaEntregaEstimada { get; set; }
    public decimal? Anticipo { get; set; }
    public string? Notas { get; set; }
}
public class UpdatePedidoDto
{
    public int? ArtesanaId { get; set; }
    public decimal? PrecioTotal { get; set; }
    public string? Estado { get; set; }
    public string? Notas { get; set; }
}