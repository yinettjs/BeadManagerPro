using BeadManagerPro.Application.Dtos.Usuarios;
using BeadManagerPro.Application.Exceptions;

namespace BeadManagerPro.Application.Validator
{
    public static class ArtesanaValidator 
    {
        public static void Validate(CreateArtesanaDto dto) 
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                errors.Add("El nombre de la artesana es obligatorio.");

            if (string.IsNullOrWhiteSpace(dto.Apellido))
                errors.Add("El apellido de la artesana es obligatorio.");

            if (string.IsNullOrWhiteSpace(dto.Telefono))
                errors.Add("El teléfono de la artesana es obligatorio.");

            if (errors.Count > 0)
                throw new ValidationException(errors);
        }
    }
}
