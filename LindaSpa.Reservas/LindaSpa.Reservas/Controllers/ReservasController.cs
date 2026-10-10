using LindaSpa.Reservas.DTOs;
using LindaSpa.Reservas.Services;
using Microsoft.AspNetCore.Mvc;

namespace LindaSpa.Reservas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservasController : ControllerBase
{
	private readonly IReservaService _reservaService;

	public ReservasController(IReservaService reservaService)
	{
		_reservaService = reservaService;
	}

	[HttpPost]
	public ActionResult<ReservaResponseDto> Crear(ReservaCreateDto dto)
	{
		var (reserva, error) = _reservaService.Crear(dto);

		if (error is not null)
			return BadRequest(new { error });

		return CreatedAtAction(nameof(ObtenerPorId), new { id = reserva!.Id }, reserva);
	}

	[HttpGet]
	public ActionResult<IEnumerable<ReservaResponseDto>> Listar() =>
		Ok(_reservaService.Listar());

	[HttpGet("{id}")]
	public ActionResult<ReservaResponseDto> ObtenerPorId(int id)
	{
		var reserva = _reservaService.ObtenerPorId(id);
		return reserva is null ? NotFound() : Ok(reserva);
	}
}