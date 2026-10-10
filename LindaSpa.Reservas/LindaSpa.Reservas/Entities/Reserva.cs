namespace LindaSpa.Reservas.Entities;

public class Reserva
{
	public int Id { get; set; }
	public int ClienteId { get; set; }
	public int ServicioId { get; set; }
	public int EspecialistaId { get; set; }
	public DateTime FechaHora { get; set; }
}