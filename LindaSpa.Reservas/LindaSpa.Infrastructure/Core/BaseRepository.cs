using LindaSpa.Domain.Core;
using LindaSpa.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LindaSpa.Infrastructure.Core;

public class BaseRepository<T> where T : BaseEntity
{
	protected readonly LindaSpaContext _context;
	protected readonly DbSet<T> _entities;

	public BaseRepository(LindaSpaContext context)
	{
		_context = context;
		_entities = context.Set<T>();
	}

	public virtual List<T> GetAll()
	{
		return _entities.AsNoTracking().ToList();
	}

	public virtual T? GetById(int id)
	{
		return _entities.Find(id);
	}

	public virtual bool Exists(int id)
	{
		return _entities.Any(e => e.Id == id);
	}

	public virtual void Add(T entity)
	{
		_entities.Add(entity);
		_context.SaveChanges();
	}

	public virtual void Update(T entity)
	{
		_entities.Update(entity);
		_context.SaveChanges();
	}

	public virtual void Remove(T entity)
	{
		_entities.Remove(entity);
		_context.SaveChanges();
	}
}
