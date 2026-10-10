using LindaSpa.Reservas.DTOs;
using LindaSpa.Reservas.Services;
using Microsoft.AspNetCore.Mvc;

namespace LindaSpa.Reservas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiciosController : ControllerBase
{
	private readonly IServicioService _servicioService;

	public ServiciosController(IServicioService servicioService)
	{
		_servicioService = servicioService;
	}

	[HttpPost]
	public ActionResult<ServicioResponseDto> Crear(ServicioCreateDto dto)
	{
		var creado = _servicioService.Crear(dto);
		return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
	}

	[HttpGet]
	public ActionResult<IEnumerable<ServicioResponseDto>> Listar() =>
		Ok(_servicioService.Listar());

	[HttpGet("{id}")]
	public ActionResult<ServicioResponseDto> ObtenerPorId(int id)
	{
		var servicio = _servicioService.ObtenerPorId(id);
		return servicio is null ? NotFound() : Ok(servicio);
	}
}