using LindaSpa.Reservas.DTOs;

namespace LindaSpa.Reservas.Services;

public interface IEspecialistaService
{
	EspecialistaResponseDto Crear(EspecialistaCreateDto dto);
	IEnumerable<EspecialistaResponseDto> Listar();
	EspecialistaResponseDto? ObtenerPorId(int id);
}