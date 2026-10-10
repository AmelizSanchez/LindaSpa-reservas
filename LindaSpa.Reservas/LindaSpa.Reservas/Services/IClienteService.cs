using LindaSpa.Reservas.DTOs;

namespace LindaSpa.Reservas.Services;

public interface IClienteService
{
	ClienteResponseDto Crear(ClienteCreateDto dto);
	IEnumerable<ClienteResponseDto> Listar();
	ClienteResponseDto? ObtenerPorId(int id);
}