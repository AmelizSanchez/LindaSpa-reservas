using LindaSpa.Domain.Core;

namespace LindaSpa.Domain.Entities;

public class Especialista : BaseEntity
{
	public string Nombre { get; set; } = string.Empty;
	public string Especialidad { get; set; } = string.Empty;
}
