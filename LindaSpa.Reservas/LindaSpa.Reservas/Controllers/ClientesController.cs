using LindaSpa.Domain.Entities;
using LindaSpa.Infrastructure.Core;
using LindaSpa.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LindaSpa.Reservas.Controllers;

[ApiController]
[Route("api/clientes")]
public class ClientesController : ControllerBase
{
	private readonly ClienteRepository _clienteRepository;

	public ClientesController(ClienteRepository clienteRepository)
	{
		_clienteRepository = clienteRepository;
	}

	[HttpGet]
	public ActionResult<List<Cliente>> GetAll()
	{
		return Ok(_clienteRepository.GetAll());
	}

	[HttpGet("{id}")]
	public ActionResult<Cliente> GetById(int id)
	{
		var cliente = _clienteRepository.GetById(id);
		if (cliente == null)
		{
			return NotFound();
		}

		return Ok(cliente);
	}

	[HttpPost]
	public ActionResult<Cliente> Create([FromBody] Cliente request)
	{
		request.Id = 0;

		var error = Validar(request);
		if (error != null)
		{
			return BadRequest(error);
		}

		_clienteRepository.Add(request);

		return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);
	}

	[HttpPut("{id}")]
	public IActionResult Update(int id, [FromBody] Cliente request)
	{
		var cliente = _clienteRepository.GetById(id);
		if (cliente == null)
		{
			return NotFound();
		}

		cliente.Nombre = request.Nombre;
		cliente.Apellido = request.Apellido;
		cliente.Telefono = request.Telefono;
		cliente.Email = request.Email;

		var error = Validar(cliente);
		if (error != null)
		{
			return BadRequest(error);
		}

		_clienteRepository.Update(cliente);

		return NoContent();
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		var cliente = _clienteRepository.GetById(id);
		if (cliente == null)
		{
			return NotFound();
		}

		_clienteRepository.Remove(cliente);

		return NoContent();
	}

	private string? Validar(Cliente cliente)
	{
		if (string.IsNullOrWhiteSpace(cliente.Nombre))
		{
			return "El nombre es obligatorio.";
		}
		if (string.IsNullOrWhiteSpace(cliente.Apellido))
		{
			return "El apellido es obligatorio.";
		}
		if (string.IsNullOrWhiteSpace(cliente.Telefono))
		{
			return "El teléfono es obligatorio.";
		}
		if (string.IsNullOrWhiteSpace(cliente.Email))
		{
			return "El email es obligatorio.";
		}
		if (_clienteRepository.EmailExists(cliente.Email, cliente.Id))
		{
			return "Ya existe un cliente con ese email.";
		}

		return null;
	}
}
