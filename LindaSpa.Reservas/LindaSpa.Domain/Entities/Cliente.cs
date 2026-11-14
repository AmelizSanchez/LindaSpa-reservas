using LindaSpa.Domain.Core;

namespace LindaSpa.Domain.Entities;

public class Cliente : BaseEntity
{
	public string Nombre { get; set; } = string.Empty;
	public string Apellido { get; set; } = string.Empty;
	public string Telefono { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
}
