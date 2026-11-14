using System.Text.RegularExpressions;
using LindaSpa.Application.Core;
using LindaSpa.Application.Dtos;
using LindaSpa.Domain.Entities;
using LindaSpa.Infrastructure.Core;
using LindaSpa.Infrastructure.Repositories;

namespace LindaSpa.Application.Services;

public class ClienteService
{
	private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
	private static readonly Regex TelefonoRegex = new(@"^\+?[0-9\s\-\(\)]{7,15}$", RegexOptions.Compiled);

	private readonly ClienteRepository _clienteRepository;

	public ClienteService(ClienteRepository clienteRepository)
	{
		_clienteRepository = clienteRepository;
	}

	public ServiceResult<List<ClienteDto>> GetAll()
	{
		var lista = _clienteRepository.GetAll().Select(ToDto).ToList();
		return ServiceResult<List<ClienteDto>>.Ok(lista);
	}

	public ServiceResult<ClienteDto> GetById(int id)
	{
		var cliente = _clienteRepository.GetById(id);
		if (cliente is null)
		{
			return ServiceResult<ClienteDto>.Fail("El cliente no existe.", 404);
		}

		return ServiceResult<ClienteDto>.Ok(ToDto(cliente));
	}

	public ServiceResult<ClienteDto> Add(ClienteDto dto)
	{
		var errores = Validar(dto.Nombre, dto.Apellido, dto.Telefono, dto.Email, 0);
		if (errores.Count > 0)
		{
			return ServiceResult<ClienteDto>.Fail(string.Join(" ", errores));
		}

		var nuevo = new Cliente
		{
			Nombre = dto.Nombre.Trim(),
			Apellido = dto.Apellido.Trim(),
			Telefono = dto.Telefono.Trim(),
			Email = dto.Email.Trim()
		};

		_clienteRepository.Add(nuevo);

		return ServiceResult<ClienteDto>.Ok(ToDto(nuevo), "El cliente se creó correctamente.", 201);
	}

	public ServiceResult<ClienteDto> Update(int id, ClienteDto dto)
	{
		var cliente = _clienteRepository.GetById(id);
		if (cliente is null)
		{
			return ServiceResult<ClienteDto>.Fail("El cliente no existe.", 404);
		}

		var errores = Validar(dto.Nombre, dto.Apellido, dto.Telefono, dto.Email, id);
		if (errores.Count > 0)
		{
			return ServiceResult<ClienteDto>.Fail(string.Join(" ", errores));
		}

		cliente.Nombre = dto.Nombre.Trim();
		cliente.Apellido = dto.Apellido.Trim();
		cliente.Telefono = dto.Telefono.Trim();
		cliente.Email = dto.Email.Trim();

		_clienteRepository.Update(cliente);

		return ServiceResult<ClienteDto>.Ok(ToDto(cliente), "El cliente se actualizó correctamente.");
	}

	public ServiceResult<bool> Remove(int id)
	{
		var cliente = _clienteRepository.GetById(id);
		if (cliente is null)
		{
			return ServiceResult<bool>.Fail("El cliente no existe.", 404);
		}

		_clienteRepository.Remove(cliente);

		return ServiceResult<bool>.Ok(true, "El cliente se eliminó correctamente.");
	}

	private List<string> Validar(string nombre, string apellido, string telefono, string email, int idActual)
	{
		var errores = new List<string>();

		if (string.IsNullOrWhiteSpace(nombre))
		{
			errores.Add("El nombre es obligatorio.");
		}
		else if (nombre.Trim().Length > 100)
		{
			errores.Add("El nombre no puede tener más de 100 caracteres.");
		}

		if (string.IsNullOrWhiteSpace(apellido))
		{
			errores.Add("El apellido es obligatorio.");
		}
		else if (apellido.Trim().Length > 100)
		{
			errores.Add("El apellido no puede tener más de 100 caracteres.");
		}

		if (string.IsNullOrWhiteSpace(telefono))
		{
			errores.Add("El teléfono es obligatorio.");
		}
		else if (!TelefonoRegex.IsMatch(telefono.Trim()))
		{
			errores.Add("El teléfono no tiene un formato válido (7 a 15 dígitos).");
		}

		if (string.IsNullOrWhiteSpace(email))
		{
			errores.Add("El email es obligatorio.");
		}
		else if (!EmailRegex.IsMatch(email.Trim()))
		{
			errores.Add("El email no tiene un formato válido.");
		}
		else if (_clienteRepository.EmailExists(email, idActual))
		{
			errores.Add("Ya existe un cliente con ese email.");
		}

		return errores;
	}

	private static ClienteDto ToDto(Cliente cliente) => new()
	{
		Id = cliente.Id,
		Nombre = cliente.Nombre,
		Apellido = cliente.Apellido,
		Telefono = cliente.Telefono,
		Email = cliente.Email
	};
}
