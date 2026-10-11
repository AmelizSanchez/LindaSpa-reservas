using LindaSpa.Reservas.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LindaSpa.Reservas.Data;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}

	public DbSet<Cliente> Clientes { get; set; } = null!;
	public DbSet<Servicio> Servicios { get; set; } = null!;
	public DbSet<Especialista> Especialistas { get; set; } = null!;
	public DbSet<Reserva> Reservas { get; set; } = null!;
}