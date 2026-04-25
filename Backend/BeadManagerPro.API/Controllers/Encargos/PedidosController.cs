using Microsoft.AspNetCore.Mvc;
using BeadManagerPro.Application.Services;
using BeadManagerPro.Application.Dtos.Encargos;

namespace BeadManagerPro.API.Controllers.Encargos;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly PedidoService _pedidoService;

    public PedidosController(PedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PedidoDto>>> GetAll()
    {
        var pedidos = await _pedidoService.ObtenerTodosLosPedidos();
        return Ok(pedidos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PedidoDto>> GetById(int id)
    {
        var pedido = await _pedidoService.ObtenerPorId(id);
        if (pedido == null) return NotFound();
        return Ok(pedido);
    }

    [HttpPost]
    public async Task<ActionResult> Create(PedidoDto pedidoDto)
    {
        var creado = await _pedidoService.CrearPedido(pedidoDto);
        if (!creado) return BadRequest("No se pudo crear el pedido.");
        return Ok(new { mensaje = "Pedido de bisutería creado con éxito" });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var eliminado = await _pedidoService.EliminarPedido(id);
        if (!eliminado) return NotFound();
        return NoContent();
    }
}