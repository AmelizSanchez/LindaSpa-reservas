using LindaSpa.Domain.Entities;
using LindaSpa.Infrastructure.Core;
using LindaSpa.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LindaSpa.Reservas.Controllers;

[ApiController]
[Route("api/especialistas")]
public class EspecialistasController : ControllerBase
{
	private readonly BaseRepository<Especialista> _especialistaRepository;

	public EspecialistasController(BaseRepository<Especialista> especialistaRepository)
	{
		_especialistaRepository = especialistaRepository;
	}

	[HttpGet]
	public ActionResult<List<Especialista>> GetAll()
	{
		return Ok(_especialistaRepository.GetAll());
	}

	[HttpGet("{id}")]
	public ActionResult<Especialista> GetById(int id)
	{
		var especialista = _especialistaRepository.GetById(id);
		if (especialista == null)
		{
			return NotFound();
		}

		return Ok(especialista);
	}

	[HttpPost]
	public ActionResult<Especialista> Create([FromBody] Especialista request)
	{
		request.Id = 0;

		var error = Validar(request);
		if (error != null)
		{
			return BadRequest(error);
		}

		_especialistaRepository.Add(request);

		return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);
	}

	[HttpPut("{id}")]
	public IActionResult Update(int id, [FromBody] Especialista request)
	{
		var especialista = _especialistaRepository.GetById(id);
		if (especialista == null)
		{
			return NotFound();
		}

		especialista.Nombre = request.Nombre;
		especialista.Especialidad = request.Especialidad;

		var error = Validar(especialista);
		if (error != null)
		{
			return BadRequest(error);
		}

		_especialistaRepository.Update(especialista);

		return NoContent();
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		var especialista = _especialistaRepository.GetById(id);
		if (especialista == null)
		{
			return NotFound();
		}

		_especialistaRepository.Remove(especialista);

		return NoContent();
	}

	private string? Validar(Especialista especialista)
	{
		if (string.IsNullOrWhiteSpace(especialista.Nombre))
		{
			return "El nombre es obligatorio.";
		}
		if (string.IsNullOrWhiteSpace(especialista.Especialidad))
		{
			return "La especialidad es obligatoria.";
		}

		return null;
	}
}
