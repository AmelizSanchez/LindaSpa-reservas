using LindaSpa.Domain.Entities;
using LindaSpa.Infrastructure.Context;
using LindaSpa.Infrastructure.Core;

namespace LindaSpa.Infrastructure.Repositories;

public class ClienteRepository : BaseRepository<Cliente>
{
	public ClienteRepository(LindaSpaContext context) : base(context)
	{
	}

	public bool EmailExists(string email, int idActual = 0)
	{
		var correo = email.Trim().ToLower();
		return _entities.Any(c => c.Id != idActual && c.Email.ToLower() == correo);
	}
}
