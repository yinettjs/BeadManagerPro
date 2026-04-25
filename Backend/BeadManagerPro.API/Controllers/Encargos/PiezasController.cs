using Microsoft.AspNetCore.Mvc;
using BeadManagerPro.Application.Services;
using BeadManagerPro.Application.Dtos.Encargos;

namespace BeadManagerPro.API.Controllers.Produccion;

[ApiController]
[Route("api/[controller]")]
public class PiezasController : ControllerBase
{
    private readonly PiezaService _piezaService;

    public PiezasController(PiezaService piezaService)
    {
        _piezaService = piezaService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PiezaDto>>> GetAll()
    {
        var piezas = await _piezaService.ObtenerTodasLasPiezas();
        return Ok(piezas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PiezaDto>> GetById(int id)
    {
        var pieza = await _piezaService.ObtenerPorId(id);
        if (pieza == null) return NotFound();
        return Ok(pieza);
    }

    [HttpPost]
    public async Task<ActionResult> Create(PiezaDto piezaDto)
    {
        var creado = await _piezaService.CrearPieza(piezaDto);
        if (!creado) return BadRequest("No se pudo crear la pieza.");
        return Ok(new { mensaje = "Nueva pieza de colección creada con éxito" });
    }
}