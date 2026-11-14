using LindaSpa.Domain.Core;

namespace LindaSpa.Domain.Entities;

public class Reserva : BaseEntity
{
	public int ClienteId { get; set; }
	public int ServicioId { get; set; }
	public int EspecialistaId { get; set; }
	public DateTime FechaHora { get; set; }
}
