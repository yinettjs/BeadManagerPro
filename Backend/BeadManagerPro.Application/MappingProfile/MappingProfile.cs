using AutoMapper;
using BeadManagerPro.Application.Dtos.Encargos;
using BeadManagerPro.Application.Dtos.Usuarios;
using BeadManagerPro.Domain.Entities.Encargos;
using BeadManagerPro.Domain.Entities.Usuarios;

namespace BeadManagerPro.Application.MappingProfile;

public class BeadManagerProMappingProfile : Profile
{
    public BeadManagerProMappingProfile()
    {
        // --- CLIENTES ---
        CreateMap<Cliente, ClienteDto>()
            .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => $"{src.Nombre} {src.Apellido}"));
        CreateMap<CreateClienteDto, Cliente>(); // Necesario para crear clientes

        // --- ARTESANAS ---
        CreateMap<Artesana, ArtesanaDto>();
        CreateMap<CreateArtesanaDto, Artesana>(); // Necesario para crear artesanas

        // --- PEDIDOS ---
        // Mapa para mostrar datos
        CreateMap<Pedido, PedidoDto>()
            .ForMember(dest => dest.NombreCliente,
                opt => opt.MapFrom(src => $"{src.Cliente.Nombre} {src.Cliente.Apellido}"))
            .ForMember(dest => dest.NombreArtesana,
                opt => opt.MapFrom(src => src.Artesana.Nombre));

        // Mapas para recibir datos (Creación y Actualización)
        CreateMap<CreatePedidoDto, Pedido>();
        CreateMap<UpdatePedidoDto, Pedido>();

        // --- PIEZAS Y MATERIALES ---
        CreateMap<Pieza, PiezaDto>()
            .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => src.PrecioVenta));
        CreateMap<CreatePiezaDto, Pieza>(); // Si usas DTO para crear piezas

        CreateMap<Material, MaterialDto>();
        CreateMap<CreateMaterialDto, Material>(); // Si usas DTO para materiales
    }
}