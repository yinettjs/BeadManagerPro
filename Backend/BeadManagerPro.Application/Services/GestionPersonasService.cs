using AutoMapper;
using BeadManagerPro.Application.Dtos.Usuarios;
using BeadManagerPro.Application.Exceptions;
using BeadManagerPro.Application.Responses;
using BeadManagerPro.Application.Validator;
using BeadManagerPro.Application.Validators;
using BeadManagerPro.Domain.Entities.Usuarios;
using BeadManagerPro.Infrastructure.Repositories;

namespace BeadManagerPro.Application.Services;

public class GestionPersonasService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GestionPersonasService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    // --- SECCIÓN CLIENTES ---
    public async Task<ApiResponse<List<ClienteDto>>> GetAllClientesAsync()
    {
        var clientes = await _uow.Clientes.GetAllAsync();
        return ApiResponse<List<ClienteDto>>.Ok(_mapper.Map<List<ClienteDto>>(clientes));
    }

    public async Task<ApiResponse<ClienteDto>> GetClienteByIdAsync(int id)
    {
        var cliente = await _uow.Clientes.GetByIdAsync(id)
            ?? throw new NotFoundException("Cliente", id);

        return ApiResponse<ClienteDto>.Ok(_mapper.Map<ClienteDto>(cliente));
    }

    public async Task<ApiResponse<ClienteDto>> CreateClienteAsync(CreateClienteDto dto)
    {
        ClienteValidator.Validate(dto);

        var cliente = _mapper.Map<Cliente>(dto);
        await _uow.Clientes.AddAsync(cliente);
        await _uow.SaveChangesAsync();

        return ApiResponse<ClienteDto>.Ok(_mapper.Map<ClienteDto>(cliente), "Cliente registrado.");
    }

    public async Task<ApiResponse<ClienteDto>> UpdateClienteAsync(int id, CreateClienteDto dto)
    {
        ClienteValidator.Validate(dto);

        var cliente = await _uow.Clientes.GetByIdAsync(id)
            ?? throw new NotFoundException("Cliente", id);

        cliente.Nombre = dto.Nombre;
        cliente.Apellido = dto.Apellido;
        cliente.Telefono = dto.Telefono;
        cliente.Correo = dto.Correo;

        _uow.Clientes.Update(cliente);
        await _uow.SaveChangesAsync();

        return ApiResponse<ClienteDto>.Ok(_mapper.Map<ClienteDto>(cliente), "Cliente actualizado.");
    }

    public async Task<ApiResponse<bool>> DeleteClienteAsync(int id)
    {
        var cliente = await _uow.Clientes.GetByIdAsync(id)
            ?? throw new NotFoundException("Cliente", id);

        cliente.IsDeleted = true;
        _uow.Clientes.Update(cliente);
        await _uow.SaveChangesAsync();

        return ApiResponse<bool>.Ok(true, "Cliente eliminado.");
    }

    // --- SECCIÓN ARTESANAS ---
    public async Task<ApiResponse<List<ArtesanaDto>>> GetAllArtesanasAsync()
    {
        var artesanas = await _uow.Artesanas.GetAllWithPedidosAsync();
        return ApiResponse<List<ArtesanaDto>>.Ok(_mapper.Map<List<ArtesanaDto>>(artesanas));
    }

    public async Task<ApiResponse<ArtesanaDto>> GetArtesanaByIdAsync(int id)
    {
        var artesana = await _uow.Artesanas.GetByIdAsync(id)
            ?? throw new NotFoundException("Artesana", id);

        return ApiResponse<ArtesanaDto>.Ok(_mapper.Map<ArtesanaDto>(artesana));
    }

    public async Task<ApiResponse<ArtesanaDto>> CreateArtesanaAsync(CreateArtesanaDto dto)
    {
        ArtesanaValidator.Validate(dto);

        var artesana = _mapper.Map<Artesana>(dto);
        await _uow.Artesanas.AddAsync(artesana);
        await _uow.SaveChangesAsync();

        return ApiResponse<ArtesanaDto>.Ok(_mapper.Map<ArtesanaDto>(artesana), "Artesana registrada.");
    }

    public async Task<ApiResponse<bool>> ToggleActivaAsync(int id)
    {
        var artesana = await _uow.Artesanas.GetByIdAsync(id)
            ?? throw new NotFoundException("Artesana", id);

        // Si aquí sigue dando error, ve a la clase Artesana.cs y añade: public bool Activa { get; set; }
        artesana.Activa = !artesana.Activa;

        _uow.Artesanas.Update(artesana);
        await _uow.SaveChangesAsync();

        var estado = artesana.Activa ? "activada" : "desactivada";
        return ApiResponse<bool>.Ok(artesana.Activa, $"Artesana {estado}.");
    }

    public async Task<ApiResponse<bool>> DeleteArtesanaAsync(int id)
    {
        var artesana = await _uow.Artesanas.GetByIdAsync(id)
            ?? throw new NotFoundException("Artesana", id);

        artesana.IsDeleted = true;
        _uow.Artesanas.Update(artesana);
        await _uow.SaveChangesAsync();

        return ApiResponse<bool>.Ok(true, "Artesana eliminada.");
    }
}
