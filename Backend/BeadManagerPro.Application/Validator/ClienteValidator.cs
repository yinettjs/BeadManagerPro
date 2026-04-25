using BeadManagerPro.Application.Dtos.Usuarios;
using BeadManagerPro.Application.Exceptions;

public static class ClienteValidator
{
    public static void Validate(CreateClienteDto dto)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(dto.Nombre))
            errors.Add("El nombre del cliente es obligatorio.");

        if (string.IsNullOrWhiteSpace(dto.Apellido))
            errors.Add("El apellido del cliente es obligatorio.");

        if (string.IsNullOrWhiteSpace(dto.Telefono))
            errors.Add("El teléfono de contacto es obligatorio.");

        if (errors.Count > 0)
            throw new ValidationException(errors);
    }
}