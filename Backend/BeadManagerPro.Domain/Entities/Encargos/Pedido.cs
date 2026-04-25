using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeadManagerPro.Domain.Entities.Usuarios;

namespace BeadManagerPro.Domain.Entities.Encargos;
public class Pedido
{
    public int Id { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public DateTime? FechaEntregaComprometida { get; set; }
    public string Estado { get; set; } = "Pendiente"; // Pendiente, En Proceso, Terminado, Entregado
    public decimal TotalPagar { get; set; }
    public bool IsDeleted { get; set; }

    // Relaciones (Foreign Keys)
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;

    public int ArtesanaId { get; set; }
    public Artesana Artesana { get; set; } = null!;
}