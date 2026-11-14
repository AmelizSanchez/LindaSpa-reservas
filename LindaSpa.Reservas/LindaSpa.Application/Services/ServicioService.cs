using LindaSpa.Application.Core;
using LindaSpa.Application.Dtos;
using LindaSpa.Domain.Entities;
using LindaSpa.Infrastructure.Core;
using LindaSpa.Infrastructure.Repositories;

namespace LindaSpa.Application.Services;

public class ServicioService
{
	private readonly BaseRepository<Servicio> _servicioRepository;

	public ServicioService(BaseRepository<Servicio> servicioRepository)
	{
		_servicioRepository = servicioRepository;
	}

	public ServiceResult<List<ServicioDto>> GetAll()
	{
		var lista = _servicioRepository.GetAll().Select(ToDto).ToList();
		return ServiceResult<List<ServicioDto>>.Ok(lista);
	}

	public ServiceResult<ServicioDto> GetById(int id)
	{
		var servicio = _servicioRepository.GetById(id);
		if (servicio is null)
		{
			return ServiceResult<ServicioDto>.Fail("El servicio no existe.", 404);
		}

		return ServiceResult<ServicioDto>.Ok(ToDto(servicio));
	}

	public ServiceResult<ServicioDto> Add(ServicioDto dto)
	{
		var errores = Validar(dto.Nombre, dto.DuracionMinutos, dto.Precio, 0);
		if (errores.Count > 0)
		{
			return ServiceResult<ServicioDto>.Fail(string.Join(" ", errores));
		}

		var nuevo = new Servicio
		{
			Nombre = dto.Nombre.Trim(),
			DuracionMinutos = dto.DuracionMinutos,
			Precio = dto.Precio
		};

		_servicioRepository.Add(nuevo);

		return ServiceResult<ServicioDto>.Ok(ToDto(nuevo), "El servicio se creó correctamente.", 201);
	}

	public ServiceResult<ServicioDto> Update(int id, ServicioDto dto)
	{
		var servicio = _servicioRepository.GetById(id);
		if (servicio is null)
		{
			return ServiceResult<ServicioDto>.Fail("El servicio no existe.", 404);
		}

		var errores = Validar(dto.Nombre, dto.DuracionMinutos, dto.Precio, id);
		if (errores.Count > 0)
		{
			return ServiceResult<ServicioDto>.Fail(string.Join(" ", errores));
		}

		servicio.Nombre = dto.Nombre.Trim();
		servicio.DuracionMinutos = dto.DuracionMinutos;
		servicio.Precio = dto.Precio;

		_servicioRepository.Update(servicio);

		return ServiceResult<ServicioDto>.Ok(ToDto(servicio), "El servicio se actualizó correctamente.");
	}

	public ServiceResult<bool> Remove(int id)
	{
		var servicio = _servicioRepository.GetById(id);
		if (servicio is null)
		{
			return ServiceResult<bool>.Fail("El servicio no existe.", 404);
		}

		_servicioRepository.Remove(servicio);

		return ServiceResult<bool>.Ok(true, "El servicio se eliminó correctamente.");
	}

	private List<string> Validar(string nombre, int duracionMinutos, decimal precio, int idActual)
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
		else if (_servicioRepository.GetAll().Any(s => s.Id != idActual
			&& s.Nombre.Equals(nombre.Trim(), StringComparison.OrdinalIgnoreCase)))
		{
			errores.Add("Ya existe un servicio con ese nombre.");
		}

		if (duracionMinutos < 5 || duracionMinutos > 480)
		{
			errores.Add("La duración debe estar entre 5 y 480 minutos.");
		}

		if (precio <= 0)
		{
			errores.Add("El precio debe ser mayor que cero.");
		}
		else if (precio > 1000000)
		{
			errores.Add("El precio no puede superar 1,000,000.");
		}

		return errores;
	}

	private static ServicioDto ToDto(Servicio servicio) => new()
	{
		Id = servicio.Id,
		Nombre = servicio.Nombre,
		DuracionMinutos = servicio.DuracionMinutos,
		Precio = servicio.Precio
	};
}
