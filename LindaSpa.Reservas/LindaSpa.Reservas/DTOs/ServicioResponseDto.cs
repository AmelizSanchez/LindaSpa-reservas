namespace LindaSpa.Reservas.DTOs;

public class ServicioResponseDto
{
	public int Id { get; set; }
	public string Nombre { get; set; } = string.Empty;
	public int DuracionMinutos { get; set; }
	public decimal Precio { get; set; }
}