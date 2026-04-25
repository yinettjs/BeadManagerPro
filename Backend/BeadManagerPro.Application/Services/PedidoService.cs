using AutoMapper;
using BeadManagerPro.Application.Dtos.Encargos;
using BeadManagerPro.Domain.Entities.Encargos;
using BeadManagerPro.Infrastructure.Repositories;

namespace BeadManagerPro.Application.Services;

public class PedidoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PedidoService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // 1. Obtener todos los pedidos con sus detalles (Cliente y Artesana)
    public async Task<List<PedidoDto>> ObtenerTodosLosPedidos()
    {
        var pedidos = await _unitOfWork.Pedidos.GetAllWithDetailsAsync();
        return _mapper.Map<List<PedidoDto>>(pedidos);
    }

    // 2. Obtener un pedido por ID
    public async Task<PedidoDto?> ObtenerPorId(int id)
    {
        var pedido = await _unitOfWork.Pedidos.GetWithDetailsByIdAsync(id);
        return _mapper.Map<PedidoDto>(pedido);
    }

    // 3. Crear un nuevo pedido
    public async Task<bool> CrearPedido(PedidoDto pedidoDto)
    {
        var nuevoPedido = _mapper.Map<Pedido>(pedidoDto);

        await _unitOfWork.Pedidos.AddAsync(nuevoPedido);
        var resultado = await _unitOfWork.SaveChangesAsync();

        return resultado > 0;
    }

    // 4. Eliminar (Borrado lógico)
    public async Task<bool> EliminarPedido(int id)
    {
        var pedido = await _unitOfWork.Pedidos.GetByIdAsync(id);
        if (pedido == null) return false;

        pedido.IsDeleted = true;
        await _unitOfWork.Pedidos.UpdateAsync(pedido);

        return await _unitOfWork.SaveChangesAsync() > 0;
    }
}