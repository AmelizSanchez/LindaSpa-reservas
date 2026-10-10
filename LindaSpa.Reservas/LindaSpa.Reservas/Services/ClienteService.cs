using LindaSpa.Reservas.DTOs;
using LindaSpa.Reservas.Entities;

namespace LindaSpa.Reservas.Services;

public class ClienteService : IClienteService
{
	// Almacenamiento en memoria: la base de datos se conecta en la Tarea 2
	private readonly List<Cliente> _clientes = new();
	private int _siguienteId = 1;

	public ClienteResponseDto Crear(ClienteCreateDto dto)
	{
		var cliente = new Cliente
		{
			Id = _siguienteId++,
			Nombre = dto.Nombre,
			Apellido = dto.Apellido,
			Telefono = dto.Telefono,
			Email = dto.Email
		};

		_clientes.Add(cliente);
		return ToDto(cliente);
	}

	public IEnumerable<ClienteResponseDto> Listar() =>
		_clientes.Select(ToDto);

	public ClienteResponseDto? ObtenerPorId(int id)
	{
		var cliente = _clientes.FirstOrDefault(c => c.Id == id);
		return cliente is null ? null : ToDto(cliente);
	}

	private static ClienteResponseDto ToDto(Cliente c) => new()
	{
		Id = c.Id,
		Nombre = c.Nombre,
		Apellido = c.Apellido,
		Telefono = c.Telefono,
		Email = c.Email
	};
}