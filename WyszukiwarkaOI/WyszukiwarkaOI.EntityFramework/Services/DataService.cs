using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WyszukiwarkaOI.Domain.Models;
using WyszukiwarkaOI.Domain.Services;

namespace WyszukiwarkaOI.EntityFramework.Services;
public class DataService<T>(AppDbContextFactory contextFactory) : IDataService<T>
	where T : DomainObject
{
	private readonly AppDbContextFactory _contextFactory = contextFactory;

	public async Task<T> Create(T entity)
	{
		using AppDbContext context = _contextFactory.CreateDbContext();
		EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
		await context.SaveChangesAsync();

		return createdResult.Entity;
	}
	public async Task<bool> Delete(int id)
	{
		using AppDbContext context = _contextFactory.CreateDbContext();
		T? entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

		if (entity is null)
		{
			return false;
		}

		context.Set<T>().Remove(entity);
		await context.SaveChangesAsync();

		return true;
	}

	public async Task<T> Get(int id)
	{
		using AppDbContext context = _contextFactory.CreateDbContext();
		T? entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

		Type type = typeof(T);

		if (entity is null)
		{
			throw new Exception($"none {type} of given id found");
		}

		return entity;
	}

	public async Task<IEnumerable<T>> GetAll()
	{
		using (AppDbContext context = _contextFactory.CreateDbContext())
		{
			IEnumerable<T> entities = await context.Set<T>().ToListAsync();

			return entities;
		}
	}

	public async Task<T> Update(int id, T entity)
	{
		using AppDbContext context = _contextFactory.CreateDbContext();
		entity.Id = id;

		context.Set<T>().Update(entity);
		await context.SaveChangesAsync();

		return entity;
	}
}
