using LindaSpa.Reservas.DTOs;
using LindaSpa.Reservas.Entities;

namespace LindaSpa.Reservas.Services;

public class EspecialistaService : IEspecialistaService
{
	private readonly List<Especialista> _especialistas = new();
	private int _siguienteId = 1;

	public EspecialistaResponseDto Crear(EspecialistaCreateDto dto)
	{
		var especialista = new Especialista
		{
			Id = _siguienteId++,
			Nombre = dto.Nombre,
			Especialidad = dto.Especialidad
		};

		_especialistas.Add(especialista);
		return ToDto(especialista);
	}

	public IEnumerable<EspecialistaResponseDto> Listar() =>
		_especialistas.Select(ToDto);

	public EspecialistaResponseDto? ObtenerPorId(int id)
	{
		var especialista = _especialistas.FirstOrDefault(e => e.Id == id);
		return especialista is null ? null : ToDto(especialista);
	}

	private static EspecialistaResponseDto ToDto(Especialista e) => new()
	{
		Id = e.Id,
		Nombre = e.Nombre,
		Especialidad = e.Especialidad
	};
}