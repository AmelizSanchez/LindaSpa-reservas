using LindaSpa.Reservas.Data;
using LindaSpa.Reservas.Models.Dtos;
using LindaSpa.Reservas.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LindaSpa.Reservas.Controllers;

[ApiController]
[Route("api/servicios")]
public class ServiciosController : ControllerBase
{
	private readonly ApplicationDbContext _context;

	public ServiciosController(ApplicationDbContext context)
	{
		_context = context;
	}

	[HttpGet]
	public ActionResult<List<ServicioDTO>> GetAll()
	{
		var servicios = _context.Servicios.ToList();

		var servicioDTOs = servicios.Select(s => new ServicioDTO
		{
			Id = s.Id,
			Nombre = s.Nombre,
			DuracionMinutos = s.DuracionMinutos,
			Precio = s.Precio
		}).ToList();

		return Ok(servicioDTOs);
	}

	[HttpGet("{id}")]
	public ActionResult<ServicioDTO> GetById(int id)
	{
		var servicio = _context.Servicios.Find(id);
		if (servicio == null)
		{
			return NotFound();
		}

		var servicioDTO = new ServicioDTO
		{
			Id = servicio.Id,
			Nombre = servicio.Nombre,
			DuracionMinutos = servicio.DuracionMinutos,
			Precio = servicio.Precio
		};

		return Ok(servicioDTO);
	}

	[HttpPost]
	public ActionResult<ServicioDTO> Create([FromBody] CreateServicioDTO request)
	{
		if (string.IsNullOrWhiteSpace(request.Nombre))
		{
			return BadRequest("El nombre del servicio es obligatorio.");
		}
		if (request.DuracionMinutos <= 0)
		{
			return BadRequest("La duración debe ser mayor que cero.");
		}
		if (request.Precio <= 0)
		{
			return BadRequest("El precio debe ser mayor que cero.");
		}

		var nuevoServicio = new Servicio
		{
			Nombre = request.Nombre,
			DuracionMinutos = request.DuracionMinutos,
			Precio = request.Precio
		};

		_context.Servicios.Add(nuevoServicio);
		_context.SaveChanges();

		var servicioDTO = new ServicioDTO
		{
			Id = nuevoServicio.Id,
			Nombre = nuevoServicio.Nombre,
			DuracionMinutos = nuevoServicio.DuracionMinutos,
			Precio = nuevoServicio.Precio
		};

		return CreatedAtAction(nameof(GetById), new { id = nuevoServicio.Id }, servicioDTO);
	}

	[HttpPut("{id}")]
	public IActionResult Update(int id, [FromBody] CreateServicioDTO request)
	{
		if (string.IsNullOrWhiteSpace(request.Nombre))
		{
			return BadRequest("El nombre del servicio es obligatorio.");
		}
		if (request.DuracionMinutos <= 0)
		{
			return BadRequest("La duración debe ser mayor que cero.");
		}
		if (request.Precio <= 0)
		{
			return BadRequest("El precio debe ser mayor que cero.");
		}

		var servicio = _context.Servicios.Find(id);
		if (servicio == null)
		{
			return NotFound();
		}

		servicio.Nombre = request.Nombre;
		servicio.DuracionMinutos = request.DuracionMinutos;
		servicio.Precio = request.Precio;

		_context.SaveChanges();

		return NoContent();
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		var servicio = _context.Servicios.Find(id);
		if (servicio == null)
		{
			return NotFound();
		}

		_context.Servicios.Remove(servicio);
		_context.SaveChanges();

		return NoContent();
	}
}