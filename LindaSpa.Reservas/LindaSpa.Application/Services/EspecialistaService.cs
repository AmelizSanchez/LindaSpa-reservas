using LindaSpa.Application.Core;
using LindaSpa.Application.Dtos;
using LindaSpa.Domain.Entities;
using LindaSpa.Infrastructure.Core;
using LindaSpa.Infrastructure.Repositories;

namespace LindaSpa.Application.Services;

public class EspecialistaService
{
	private readonly BaseRepository<Especialista> _especialistaRepository;

	public EspecialistaService(BaseRepository<Especialista> especialistaRepository)
	{
		_especialistaRepository = especialistaRepository;
	}

	public ServiceResult<List<EspecialistaDto>> GetAll()
	{
		var lista = _especialistaRepository.GetAll().Select(ToDto).ToList();
		return ServiceResult<List<EspecialistaDto>>.Ok(lista);
	}

	public ServiceResult<EspecialistaDto> GetById(int id)
	{
		var especialista = _especialistaRepository.GetById(id);
		if (especialista is null)
		{
			return ServiceResult<EspecialistaDto>.Fail("El especialista no existe.", 404);
		}

		return ServiceResult<EspecialistaDto>.Ok(ToDto(especialista));
	}

	public ServiceResult<EspecialistaDto> Add(EspecialistaDto dto)
	{
		var errores = Validar(dto.Nombre, dto.Especialidad);
		if (errores.Count > 0)
		{
			return ServiceResult<EspecialistaDto>.Fail(string.Join(" ", errores));
		}

		var nuevo = new Especialista
		{
			Nombre = dto.Nombre.Trim(),
			Especialidad = dto.Especialidad.Trim()
		};

		_especialistaRepository.Add(nuevo);

		return ServiceResult<EspecialistaDto>.Ok(ToDto(nuevo), "El especialista se creó correctamente.", 201);
	}

	public ServiceResult<EspecialistaDto> Update(int id, EspecialistaDto dto)
	{
		var especialista = _especialistaRepository.GetById(id);
		if (especialista is null)
		{
			return ServiceResult<EspecialistaDto>.Fail("El especialista no existe.", 404);
		}

		var errores = Validar(dto.Nombre, dto.Especialidad);
		if (errores.Count > 0)
		{
			return ServiceResult<EspecialistaDto>.Fail(string.Join(" ", errores));
		}

		especialista.Nombre = dto.Nombre.Trim();
		especialista.Especialidad = dto.Especialidad.Trim();

		_especialistaRepository.Update(especialista);

		return ServiceResult<EspecialistaDto>.Ok(ToDto(especialista), "El especialista se actualizó correctamente.");
	}

	public ServiceResult<bool> Remove(int id)
	{
		var especialista = _especialistaRepository.GetById(id);
		if (especialista is null)
		{
			return ServiceResult<bool>.Fail("El especialista no existe.", 404);
		}

		_especialistaRepository.Remove(especialista);

		return ServiceResult<bool>.Ok(true, "El especialista se eliminó correctamente.");
	}

	private List<string> Validar(string nombre, string especialidad)
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

		if (string.IsNullOrWhiteSpace(especialidad))
		{
			errores.Add("La especialidad es obligatorio.");
		}
		else if (especialidad.Trim().Length > 100)
		{
			errores.Add("La especialidad no puede tener más de 100 caracteres.");
		}

		return errores;
	}

	private static EspecialistaDto ToDto(Especialista especialista) => new()
	{
		Id = especialista.Id,
		Nombre = especialista.Nombre,
		Especialidad = especialista.Especialidad
	};
}
