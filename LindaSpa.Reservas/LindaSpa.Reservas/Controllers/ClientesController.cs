using LindaSpa.Reservas.Data;
using LindaSpa.Reservas.Models.Dtos;
using LindaSpa.Reservas.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LindaSpa.Reservas.Controllers;

[ApiController]
[Route("api/clientes")]
public class ClientesController : ControllerBase
{
	private readonly ApplicationDbContext _context;

	public ClientesController(ApplicationDbContext context)
	{
		_context = context;
	}

	[HttpGet]
	public ActionResult<List<ClienteDTO>> GetAll()
	{
		var clientes = _context.Clientes.ToList();

		var clienteDTOs = clientes.Select(c => new ClienteDTO
		{
			Id = c.Id,
			Nombre = c.Nombre,
			Apellido = c.Apellido,
			Telefono = c.Telefono,
			Email = c.Email
		}).ToList();

		return Ok(clienteDTOs);
	}

	[HttpGet("{id}")]
	public ActionResult<ClienteDTO> GetById(int id)
	{
		var cliente = _context.Clientes.Find(id);
		if (cliente == null)
		{
			return NotFound();
		}

		var clienteDTO = new ClienteDTO
		{
			Id = cliente.Id,
			Nombre = cliente.Nombre,
			Apellido = cliente.Apellido,
			Telefono = cliente.Telefono,
			Email = cliente.Email
		};

		return Ok(clienteDTO);
	}

	[HttpPost]
	public ActionResult<ClienteDTO> Create([FromBody] CreateClienteDTO request)
	{
		if (string.IsNullOrWhiteSpace(request.Nombre))
		{
			return BadRequest("El nombre del cliente es obligatorio.");
		}
		if (string.IsNullOrWhiteSpace(request.Apellido))
		{
			return BadRequest("El apellido del cliente es obligatorio.");
		}

		var nuevoCliente = new Cliente
		{
			Nombre = request.Nombre,
			Apellido = request.Apellido,
			Telefono = request.Telefono,
			Email = request.Email
		};

		_context.Clientes.Add(nuevoCliente);
		_context.SaveChanges();

		var clienteDTO = new ClienteDTO
		{
			Id = nuevoCliente.Id,
			Nombre = nuevoCliente.Nombre,
			Apellido = nuevoCliente.Apellido,
			Telefono = nuevoCliente.Telefono,
			Email = nuevoCliente.Email
		};

		return CreatedAtAction(nameof(GetById), new { id = nuevoCliente.Id }, clienteDTO);
	}

	[HttpPut("{id}")]
	public IActionResult Update(int id, [FromBody] CreateClienteDTO request)
	{
		if (string.IsNullOrWhiteSpace(request.Nombre))
		{
			return BadRequest("El nombre del cliente es obligatorio.");
		}
		if (string.IsNullOrWhiteSpace(request.Apellido))
		{
			return BadRequest("El apellido del cliente es obligatorio.");
		}

		var cliente = _context.Clientes.Find(id);
		if (cliente == null)
		{
			return NotFound();
		}

		cliente.Nombre = request.Nombre;
		cliente.Apellido = request.Apellido;
		cliente.Telefono = request.Telefono;
		cliente.Email = request.Email;

		_context.SaveChanges();

		return NoContent();
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		var cliente = _context.Clientes.Find(id);
		if (cliente == null)
		{
			return NotFound();
		}

		_context.Clientes.Remove(cliente);
		_context.SaveChanges();

		return NoContent();
	}
}