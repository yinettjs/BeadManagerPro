using AutoMapper;
using BeadManagerPro.Application.Dtos.Encargos;
using BeadManagerPro.Domain.Entities.Encargos;
using BeadManagerPro.Infrastructure.Repositories;

namespace BeadManagerPro.Application.Services;

public class MaterialService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MaterialService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<MaterialDto>> ObtenerTodosLosMateriales()
    {
        var materiales = await _unitOfWork.Materiales.GetAllAsync();
        return _mapper.Map<List<MaterialDto>>(materiales);
    }

    public async Task<MaterialDto?> ObtenerPorId(int id)
    {
        var material = await _unitOfWork.Materiales.GetByIdAsync(id);
        return _mapper.Map<MaterialDto>(material);
    }

    public async Task<bool> CrearMaterial(MaterialDto materialDto)
    {
        var nuevoMaterial = _mapper.Map<Material>(materialDto);
        await _unitOfWork.Materiales.AddAsync(nuevoMaterial);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }

    public async Task<bool> EliminarMaterial(int id)
    {
        var material = await _unitOfWork.Materiales.GetByIdAsync(id);
        if (material == null) return false;

        material.IsDeleted = true;

        // SOLUCIÓN FÁCIL: Usamos Update (el que ya existe) sin await
        _unitOfWork.Materiales.Update(material);

        // Este es el que hace el trabajo sucio en la DB y ya es async
        return await _unitOfWork.SaveChangesAsync() > 0;
    }
}

