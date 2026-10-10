using LindaSpa.Reservas.DTOs;
using LindaSpa.Reservas.Services;
using Microsoft.AspNetCore.Mvc;

namespace LindaSpa.Reservas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
	private readonly IClienteService _clienteService;

	public ClientesController(IClienteService clienteService)
	{
		_clienteService = clienteService;
	}

	[HttpPost]
	public ActionResult<ClienteResponseDto> Crear(ClienteCreateDto dto)
	{
		var creado = _clienteService.Crear(dto);
		return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
	}

	[HttpGet]
	public ActionResult<IEnumerable<ClienteResponseDto>> Listar() =>
		Ok(_clienteService.Listar());

	[HttpGet("{id}")]
	public ActionResult<ClienteResponseDto> ObtenerPorId(int id)
	{
		var cliente = _clienteService.ObtenerPorId(id);
		return cliente is null ? NotFound() : Ok(cliente);
	}
}