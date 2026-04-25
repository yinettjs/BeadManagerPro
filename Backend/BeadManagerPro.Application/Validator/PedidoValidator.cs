using BeadManagerPro.Application.Dtos.Encargos;
using BeadManagerPro.Application.Exceptions;

namespace BeadManagerPro.Application.Validators;

public static class PedidoValidator
{
    // Usamos CreatePedidoDto (el nombre nuevo que elegimos)
    public static void Validate(CreatePedidoDto dto)
    {
        var errors = new List<string>();

        if (dto.ClienteId <= 0)
            errors.Add("Debe seleccionar un cliente.");

        if (dto.ArtesanaId <= 0) // Antes era CostureraId
            errors.Add("Debe seleccionar una artesana.");

        if (dto.PrecioTotal <= 0)
            errors.Add("El precio total debe ser mayor a 0.");

        if (dto.FechaEntregaEstimada <= DateTime.Today)
            errors.Add("La fecha de entrega debe ser posterior a hoy.");

        if (dto.Anticipo.HasValue && dto.Anticipo > dto.PrecioTotal)
            errors.Add("El anticipo no puede ser mayor al precio total.");

        if (errors.Count > 0)
            throw new ValidationException(errors);
    }

    // Usamos UpdatePedidoDto
    public static void ValidateUpdate(UpdatePedidoDto dto)
    {
        var errors = new List<string>();

        if (dto.ArtesanaId.HasValue && dto.ArtesanaId <= 0)
            errors.Add("Debe seleccionar una artesana válida.");

        if (dto.PrecioTotal.HasValue && dto.PrecioTotal <= 0)
            errors.Add("El precio total debe ser mayor a 0.");

        if (errors.Count > 0)
            throw new ValidationException(errors);
    }
}