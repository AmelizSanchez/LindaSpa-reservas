using LindaSpa.Domain.Entities;
using LindaSpa.Infrastructure.Core;
using LindaSpa.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LindaSpa.Reservas.Controllers;

[ApiController]
[Route("api/servicios")]
public class ServiciosController : ControllerBase
{
	private readonly BaseRepository<Servicio> _servicioRepository;

	public ServiciosController(BaseRepository<Servicio> servicioRepository)
	{
		_servicioRepository = servicioRepository;
	}

	[HttpGet]
	public ActionResult<List<Servicio>> GetAll()
	{
		return Ok(_servicioRepository.GetAll());
	}

	[HttpGet("{id}")]
	public ActionResult<Servicio> GetById(int id)
	{
		var servicio = _servicioRepository.GetById(id);
		if (servicio == null)
		{
			return NotFound();
		}

		return Ok(servicio);
	}

	[HttpPost]
	public ActionResult<Servicio> Create([FromBody] Servicio request)
	{
		request.Id = 0;

		var error = Validar(request);
		if (error != null)
		{
			return BadRequest(error);
		}

		_servicioRepository.Add(request);

		return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);
	}

	[HttpPut("{id}")]
	public IActionResult Update(int id, [FromBody] Servicio request)
	{
		var servicio = _servicioRepository.GetById(id);
		if (servicio == null)
		{
			return NotFound();
		}

		servicio.Nombre = request.Nombre;
		servicio.DuracionMinutos = request.DuracionMinutos;
		servicio.Precio = request.Precio;

		var error = Validar(servicio);
		if (error != null)
		{
			return BadRequest(error);
		}

		_servicioRepository.Update(servicio);

		return NoContent();
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		var servicio = _servicioRepository.GetById(id);
		if (servicio == null)
		{
			return NotFound();
		}

		_servicioRepository.Remove(servicio);

		return NoContent();
	}

	private string? Validar(Servicio servicio)
	{
		if (string.IsNullOrWhiteSpace(servicio.Nombre))
		{
			return "El nombre es obligatorio.";
		}
		if (servicio.DuracionMinutos <= 0)
		{
			return "La duración debe ser mayor que cero.";
		}
		if (servicio.Precio <= 0)
		{
			return "El precio debe ser mayor que cero.";
		}

		return null;
	}
}
