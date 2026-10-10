using System.ComponentModel.DataAnnotations;

namespace LindaSpa.Reservas.DTOs;

public class ReservaCreateDto
{
	[Range(1, int.MaxValue)]
	public int ClienteId { get; set; }

	[Range(1, int.MaxValue)]
	public int ServicioId { get; set; }

	[Range(1, int.MaxValue)]
	public int EspecialistaId { get; set; }

	[Required]
	public DateTime FechaHora { get; set; }
}