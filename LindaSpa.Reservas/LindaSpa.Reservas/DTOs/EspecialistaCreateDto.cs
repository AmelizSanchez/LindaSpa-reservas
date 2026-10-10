using System.ComponentModel.DataAnnotations;

namespace LindaSpa.Reservas.DTOs;

public class EspecialistaCreateDto
{
	[Required, StringLength(100)]
	public string Nombre { get; set; } = string.Empty;

	[Required, StringLength(100)]
	public string Especialidad { get; set; } = string.Empty;
}