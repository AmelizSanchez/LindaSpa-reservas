using LindaSpa.Domain.Entities;
using LindaSpa.Infrastructure.Core;
using LindaSpa.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LindaSpa.Reservas.Controllers;

[ApiController]
[Route("api/reservas")]
public class ReservasController : ControllerBase
{
	private readonly ReservaRepository _reservaRepository;
	private readonly ClienteRepository _clienteRepository;
	private readonly BaseRepository<Servicio> _servicioRepository;
	private readonly BaseRepository<Especialista> _especialistaRepository;

	public ReservasController(ReservaRepository reservaRepository,
		ClienteRepository clienteRepository,
		BaseRepository<Servicio> servicioRepository,
		BaseRepository<Especialista> especialistaRepository)
	{
		_reservaRepository = reservaRepository;
		_clienteRepository = clienteRepository;
		_servicioRepository = servicioRepository;
		_especialistaRepository = especialistaRepository;
	}

	[HttpGet]
	public ActionResult<List<Reserva>> GetAll()
	{
		return Ok(_reservaRepository.GetAll());
	}

	[HttpGet("{id}")]
	public ActionResult<Reserva> GetById(int id)
	{
		var reserva = _reservaRepository.GetById(id);
		if (reserva == null)
		{
			return NotFound();
		}

		return Ok(reserva);
	}

	[HttpPost]
	public ActionResult<Reserva> Create([FromBody] Reserva request)
	{
		request.Id = 0;

		var error = Validar(request);
		if (error != null)
		{
			return BadRequest(error);
		}

		_reservaRepository.Add(request);

		return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);
	}

	[HttpPut("{id}")]
	public IActionResult Update(int id, [FromBody] Reserva request)
	{
		var reserva = _reservaRepository.GetById(id);
		if (reserva == null)
		{
			return NotFound();
		}

		reserva.ClienteId = request.ClienteId;
		reserva.ServicioId = request.ServicioId;
		reserva.EspecialistaId = request.EspecialistaId;
		reserva.FechaHora = request.FechaHora;

		var error = Validar(reserva);
		if (error != null)
		{
			return BadRequest(error);
		}

		_reservaRepository.Update(reserva);

		return NoContent();
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		var reserva = _reservaRepository.GetById(id);
		if (reserva == null)
		{
			return NotFound();
		}

		_reservaRepository.Remove(reserva);

		return NoContent();
	}

	private string? Validar(Reserva reserva)
	{
		if (reserva.FechaHora == default)
		{
			return "La fecha y hora son obligatorias.";
		}
		if (!_clienteRepository.Exists(reserva.ClienteId))
		{
			return "El cliente no existe.";
		}

		var servicio = _servicioRepository.GetById(reserva.ServicioId);
		if (servicio == null)
		{
			return "El servicio no existe.";
		}
		if (!_especialistaRepository.Exists(reserva.EspecialistaId))
		{
			return "El especialista no existe.";
		}
		if (!_reservaRepository.EstaDisponible(reserva.EspecialistaId, reserva.FechaHora,
			servicio.DuracionMinutos, reserva.Id))
		{
			return "El horario no está disponible para este especialista.";
		}

		return null;
	}
}
