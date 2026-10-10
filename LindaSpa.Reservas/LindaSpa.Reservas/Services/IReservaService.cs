using LindaSpa.Reservas.DTOs;

namespace LindaSpa.Reservas.Services;

public interface IReservaService
{
	(ReservaResponseDto? Reserva, string? Error) Crear(ReservaCreateDto dto);
	IEnumerable<ReservaResponseDto> Listar();
	ReservaResponseDto? ObtenerPorId(int id);
}