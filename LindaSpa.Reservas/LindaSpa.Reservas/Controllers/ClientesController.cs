using LindaSpa.Application.Dtos;
using LindaSpa.Application.Services;
using LindaSpa.Reservas.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LindaSpa.Reservas.Controllers;

[ApiController]
[Route("api/clientes")]
public class ClientesController : ControllerBase
{
	private readonly ClienteService _clienteService;

	public ClientesController(ClienteService clienteService)
	{
		_clienteService = clienteService;
	}

	[HttpGet]
	public ActionResult<List<ClienteDto>> GetAll()
	{
		var result = _clienteService.GetAll();
		return Ok(result.Data);
	}

	[HttpGet("{id}")]
	public ActionResult<ClienteDto> GetById(int id)
	{
		var result = _clienteService.GetById(id);
		if (!result.Success)
		{
			return this.ToError(result);
		}

		return Ok(result.Data);
	}

	[HttpPost]
	public ActionResult<ClienteDto> Create([FromBody] ClienteDto request)
	{
		var result = _clienteService.Add(request);
		if (!result.Success)
		{
			return this.ToError(result);
		}

		return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
	}

	[HttpPut("{id}")]
	public ActionResult<ClienteDto> Update(int id, [FromBody] ClienteDto request)
	{
		var result = _clienteService.Update(id, request);
		if (!result.Success)
		{
			return this.ToError(result);
		}

		return Ok(result.Data);
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		var result = _clienteService.Remove(id);
		if (!result.Success)
		{
			return this.ToError(result);
		}

		return NoContent();
	}
}
