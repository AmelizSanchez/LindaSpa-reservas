using LindaSpa.Application.Dtos;
using LindaSpa.Application.Services;
using LindaSpa.Reservas.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LindaSpa.Reservas.Controllers;

[ApiController]
[Route("api/especialistas")]
public class EspecialistasController : ControllerBase
{
	private readonly EspecialistaService _especialistaService;

	public EspecialistasController(EspecialistaService especialistaService)
	{
		_especialistaService = especialistaService;
	}

	[HttpGet]
	public ActionResult<List<EspecialistaDto>> GetAll()
	{
		var result = _especialistaService.GetAll();
		return Ok(result.Data);
	}

	[HttpGet("{id}")]
	public ActionResult<EspecialistaDto> GetById(int id)
	{
		var result = _especialistaService.GetById(id);
		if (!result.Success)
		{
			return this.ToError(result);
		}

		return Ok(result.Data);
	}

	[HttpPost]
	public ActionResult<EspecialistaDto> Create([FromBody] EspecialistaDto request)
	{
		var result = _especialistaService.Add(request);
		if (!result.Success)
		{
			return this.ToError(result);
		}

		return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
	}

	[HttpPut("{id}")]
	public ActionResult<EspecialistaDto> Update(int id, [FromBody] EspecialistaDto request)
	{
		var result = _especialistaService.Update(id, request);
		if (!result.Success)
		{
			return this.ToError(result);
		}

		return Ok(result.Data);
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		var result = _especialistaService.Remove(id);
		if (!result.Success)
		{
			return this.ToError(result);
		}

		return NoContent();
	}
}
