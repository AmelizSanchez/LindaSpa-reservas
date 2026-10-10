using LindaSpa.Reservas.DTOs;
using LindaSpa.Reservas.Services;
using Microsoft.AspNetCore.Mvc;

namespace LindaSpa.Reservas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EspecialistasController : ControllerBase
{
	private readonly IEspecialistaService _especialistaService;

	public EspecialistasController(IEspecialistaService especialistaService)
	{
		_especialistaService = especialistaService;
	}

	[HttpPost]
	public ActionResult<EspecialistaResponseDto> Crear(EspecialistaCreateDto dto)
	{
		var creado = _especialistaService.Crear(dto);
		return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
	}

	[HttpGet]
	public ActionResult<IEnumerable<EspecialistaResponseDto>> Listar() =>
		Ok(_especialistaService.Listar());

	[HttpGet("{id}")]
	public ActionResult<EspecialistaResponseDto> ObtenerPorId(int id)
	{
		var especialista = _especialistaService.ObtenerPorId(id);
		return especialista is null ? NotFound() : Ok(especialista);
	}
}