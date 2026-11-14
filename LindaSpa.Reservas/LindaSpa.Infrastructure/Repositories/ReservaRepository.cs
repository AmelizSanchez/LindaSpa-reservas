using LindaSpa.Domain.Entities;
using LindaSpa.Infrastructure.Context;
using LindaSpa.Infrastructure.Core;

namespace LindaSpa.Infrastructure.Repositories;

public class ReservaRepository : BaseRepository<Reserva>
{
	public ReservaRepository(LindaSpaContext context) : base(context)
	{
	}

	public bool EstaDisponible(int especialistaId, DateTime fechaHora, int duracionMinutos, int reservaIdActual = 0)
	{
		var fin = fechaHora.AddMinutes(duracionMinutos);

		var existentes = (from r in _context.Reservas
						  join s in _context.Servicios on r.ServicioId equals s.Id
						  where r.EspecialistaId == especialistaId && r.Id != reservaIdActual
						  select new { r.FechaHora, s.DuracionMinutos })
						 .ToList();

		return !existentes.Any(e =>
			fechaHora < e.FechaHora.AddMinutes(e.DuracionMinutos) && fin > e.FechaHora);
	}
}
