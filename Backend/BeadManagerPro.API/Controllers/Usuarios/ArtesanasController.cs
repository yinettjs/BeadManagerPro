using Microsoft.AspNetCore.Mvc;
using BeadManagerPro.Application.Dtos.Usuarios;
using BeadManagerPro.Application.Services;

namespace BeadManagerPro.API.Controllers.Usuarios;

[ApiController]
[Route("api/[controller]")]
public class ArtesanasController : ControllerBase
{
    private readonly GestionPersonasService _service;

    public ArtesanasController(GestionPersonasService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllArtesanasAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) =>
        Ok(await _service.GetArtesanaByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateArtesanaDto dto) =>
        Ok(await _service.CreateArtesanaAsync(dto));

    [HttpPatch("{id}/toggle-activa")]
    public async Task<IActionResult> ToggleActiva(int id) =>
        Ok(await _service.ToggleActivaAsync(id));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) =>
        Ok(await _service.DeleteArtesanaAsync(id));
}