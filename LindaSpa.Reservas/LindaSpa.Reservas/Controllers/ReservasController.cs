using LindaSpa.Application.Dtos;
using LindaSpa.Application.Services;
using LindaSpa.Reservas.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LindaSpa.Reservas.Controllers;

[ApiController]
[Route("api/reservas")]
public class ReservasController : ControllerBase
{
	private readonly ReservaService _reservaService;

	public ReservasController(ReservaService reservaService)
	{
		_reservaService = reservaService;
	}

	[HttpGet]
	public ActionResult<List<ReservaDto>> GetAll()
	{
		var result = _reservaService.GetAll();
		return Ok(result.Data);
	}

	[HttpGet("{id}")]
	public ActionResult<ReservaDto> GetById(int id)
	{
		var result = _reservaService.GetById(id);
		if (!result.Success)
		{
			return this.ToError(result);
		}

		return Ok(result.Data);
	}

	[HttpPost]
	public ActionResult<ReservaDto> Create([FromBody] ReservaDto request)
	{
		var result = _reservaService.Add(request);
		if (!result.Success)
		{
			return this.ToError(result);
		}

		return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
	}

	[HttpPut("{id}")]
	public ActionResult<ReservaDto> Update(int id, [FromBody] ReservaDto request)
	{
		var result = _reservaService.Update(id, request);
		if (!result.Success)
		{
			return this.ToError(result);
		}

		return Ok(result.Data);
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		var result = _reservaService.Remove(id);
		if (!result.Success)
		{
			return this.ToError(result);
		}

		return NoContent();
	}
}
