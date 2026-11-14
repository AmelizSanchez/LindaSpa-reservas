using LindaSpa.Domain.Core;

namespace LindaSpa.Domain.Entities;

public class Servicio : BaseEntity
{
	public string Nombre { get; set; } = string.Empty;
	public int DuracionMinutos { get; set; }
	public decimal Precio { get; set; }
}
