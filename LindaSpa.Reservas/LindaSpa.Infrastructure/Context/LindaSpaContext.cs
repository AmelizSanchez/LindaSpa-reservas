using LindaSpa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LindaSpa.Infrastructure.Context;

public class LindaSpaContext : DbContext
{
	public LindaSpaContext(DbContextOptions<LindaSpaContext> options)
		: base(options)
	{
	}

	public DbSet<Cliente> Clientes { get; set; } = null!;
	public DbSet<Servicio> Servicios { get; set; } = null!;
	public DbSet<Especialista> Especialistas { get; set; } = null!;
	public DbSet<Reserva> Reservas { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Servicio>()
			.Property(s => s.Precio)
			.HasPrecision(18, 2);
	}
}
