using System.ComponentModel.DataAnnotations;

namespace LindaSpa.Reservas.DTOs;

public class ServicioCreateDto
{
	[Required, StringLength(100)]
	public string Nombre { get; set; } = string.Empty;

	[Range(1, 480)]
	public int DuracionMinutos { get; set; }

	[Range(0.01, 100000)]
	public decimal Precio { get; set; }
}