namespace LindaSpa.Reservas.Models.Dtos;

public class CreateServicioDTO
{
	public string Nombre { get; set; } = string.Empty;
	public int DuracionMinutos { get; set; }
	public decimal Precio { get; set; }
}