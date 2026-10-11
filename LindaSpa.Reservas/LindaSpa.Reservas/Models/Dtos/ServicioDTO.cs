namespace LindaSpa.Reservas.Models.Dtos;

public class ServicioDTO
{
	public int Id { get; set; }
	public string Nombre { get; set; } = string.Empty;
	public int DuracionMinutos { get; set; }
	public decimal Precio { get; set; }
}