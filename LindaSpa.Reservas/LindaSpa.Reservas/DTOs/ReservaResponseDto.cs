namespace LindaSpa.Reservas.DTOs;

public class ReservaResponseDto
{
	public int Id { get; set; }
	public int ClienteId { get; set; }
	public int ServicioId { get; set; }
	public int EspecialistaId { get; set; }
	public DateTime FechaHora { get; set; }
}