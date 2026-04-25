using Microsoft.AspNetCore.Mvc;
using BeadManagerPro.Application.Services;
using BeadManagerPro.Application.Dtos.Encargos;

namespace BeadManagerPro.API.Controllers.Inventario;

[ApiController]
[Route("api/[controller]")]
public class MaterialesController : ControllerBase
{
    private readonly MaterialService _materialService;

    public MaterialesController(MaterialService materialService)
    {
        _materialService = materialService;
    }

    [HttpGet]
    public async Task<ActionResult<List<MaterialDto>>> GetAll()
    {
        var materiales = await _materialService.ObtenerTodosLosMateriales();
        return Ok(materiales);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MaterialDto>> GetById(int id)
    {
        var material = await _materialService.ObtenerPorId(id);
        if (material == null) return NotFound();
        return Ok(material);
    }

    [HttpPost]
    public async Task<ActionResult> Create(MaterialDto materialDto)
    {
        var creado = await _materialService.CrearMaterial(materialDto);
        if (!creado) return BadRequest("No se pudo registrar el material.");
        return Ok(new { mensaje = "Material de bisutería registrado con éxito" });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var eliminado = await _materialService.EliminarMaterial(id);
        if (!eliminado) return NotFound();
        return NoContent();
    }
}
