using System.ComponentModel.DataAnnotations;

namespace LindaSpa.Reservas.DTOs;

public class ClienteCreateDto
{
	[Required, StringLength(50)]
	public string Nombre { get; set; } = string.Empty;

	[Required, StringLength(50)]
	public string Apellido { get; set; } = string.Empty;

	[Required, Phone]
	public string Telefono { get; set; } = string.Empty;

	[Required, EmailAddress]
	public string Email { get; set; } = string.Empty;
}