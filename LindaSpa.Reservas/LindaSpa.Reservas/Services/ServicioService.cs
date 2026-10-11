using LindaSpa.Reservas.DTOs;
using LindaSpa.Reservas.Models.Entities;

namespace LindaSpa.Reservas.Services;

public class ServicioService : IServicioService
{

	private readonly List<Servicio> _servicios = new();
	private int _siguienteId = 1;

	public ServicioResponseDto Crear(ServicioCreateDto dto)
	{
		var servicio = new Servicio
		{
			Id = _siguienteId++,
			Nombre = dto.Nombre,
			DuracionMinutos = dto.DuracionMinutos,
			Precio = dto.Precio
		};

		_servicios.Add(servicio);
		return ToDto(servicio);
	}

	public IEnumerable<ServicioResponseDto> Listar() =>
		_servicios.Select(ToDto);

	public ServicioResponseDto? ObtenerPorId(int id)
	{
		var servicio = _servicios.FirstOrDefault(s => s.Id == id);
		return servicio is null ? null : ToDto(servicio);
	}

	private static ServicioResponseDto ToDto(Servicio s) => new()
	{
		Id = s.Id,
		Nombre = s.Nombre,
		DuracionMinutos = s.DuracionMinutos,
		Precio = s.Precio
	};
}