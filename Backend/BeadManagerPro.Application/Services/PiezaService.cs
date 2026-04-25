using AutoMapper;
using BeadManagerPro.Application.Dtos.Encargos;
using BeadManagerPro.Domain.Entities.Encargos;
using BeadManagerPro.Infrastructure.Repositories;

namespace BeadManagerPro.Application.Services;

public class PiezaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PiezaService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<PiezaDto>> ObtenerTodasLasPiezas()
    {
        var piezas = await _unitOfWork.Piezas.GetAllAsync();
        return _mapper.Map<List<PiezaDto>>(piezas);
    }

    public async Task<PiezaDto?> ObtenerPorId(int id)
    {
        var pieza = await _unitOfWork.Piezas.GetByIdAsync(id);
        return _mapper.Map<PiezaDto>(pieza);
    }

    public async Task<bool> CrearPieza(PiezaDto piezaDto)
    {
        var nuevaPieza = _mapper.Map<Pieza>(piezaDto);
        await _unitOfWork.Piezas.AddAsync(nuevaPieza);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }
}
