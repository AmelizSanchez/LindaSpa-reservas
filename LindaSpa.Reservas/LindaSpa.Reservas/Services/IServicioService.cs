using LindaSpa.Reservas.DTOs;

namespace LindaSpa.Reservas.Services;

public interface IServicioService
{
	ServicioResponseDto Crear(ServicioCreateDto dto);
	IEnumerable<ServicioResponseDto> Listar();
	ServicioResponseDto? ObtenerPorId(int id);
}