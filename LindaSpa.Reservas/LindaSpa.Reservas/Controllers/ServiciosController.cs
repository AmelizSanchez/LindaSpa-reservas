using LindaSpa.Application.Dtos;
using LindaSpa.Application.Services;
using LindaSpa.Reservas.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LindaSpa.Reservas.Controllers;

[ApiController]
[Route("api/servicios")]
public class ServiciosController : ControllerBase
{
	private readonly ServicioService _servicioService;

	public ServiciosController(ServicioService servicioService)
	{
		_servicioService = servicioService;
	}

	[HttpGet]
	public ActionResult<List<ServicioDto>> GetAll()
	{
		var result = _servicioService.GetAll();
		return Ok(result.Data);
	}

	[HttpGet("{id}")]
	public ActionResult<ServicioDto> GetById(int id)
	{
		var result = _servicioService.GetById(id);
		if (!result.Success)
		{
			return this.ToError(result);
		}

		return Ok(result.Data);
	}

	[HttpPost]
	public ActionResult<ServicioDto> Create([FromBody] ServicioDto request)
	{
		var result = _servicioService.Add(request);
		if (!result.Success)
		{
			return this.ToError(result);
		}

		return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
	}

	[HttpPut("{id}")]
	public ActionResult<ServicioDto> Update(int id, [FromBody] ServicioDto request)
	{
		var result = _servicioService.Update(id, request);
		if (!result.Success)
		{
			return this.ToError(result);
		}

		return Ok(result.Data);
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		var result = _servicioService.Remove(id);
		if (!result.Success)
		{
			return this.ToError(result);
		}

		return NoContent();
	}
}
