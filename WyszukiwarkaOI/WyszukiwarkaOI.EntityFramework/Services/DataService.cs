using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WyszukiwarkaOI.Domain.Models;
using WyszukiwarkaOI.Domain.Services;

namespace WyszukiwarkaOI.EntityFramework.Services;
public class DataService<T> : IDataService<T>
	where T : DomainObject
{
	private readonly AppDbContextFactory _contextFactory;
	private DbContext _context;

	public DataService(AppDbContextFactory contextFactory)
	{
		_contextFactory = contextFactory;
		_context = _contextFactory.CreateDbContext();
	}

	public async Task<T> Create(T entity)
	{
		EntityEntry<T> createdResult = await _context.Set<T>().AddAsync(entity);
		await _context.SaveChangesAsync();

		return createdResult.Entity;
	}
	public async Task<bool> Delete(int id)
	{
		T? entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

		if (entity is null)
		{
			return false;
		}

		_context.Set<T>().Remove(entity);
		await _context.SaveChangesAsync();

		return true;
	}

	public async Task<T> Get(int id)
	{
		T? entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

		Type type = typeof(T);

		if (entity is null)
		{
			throw new Exception($"none {type} of given id found");
		}

		return entity;
	}

	public async Task<IEnumerable<T>> GetAll()
	{
		IEnumerable<T> entities = await _context.Set<T>().ToListAsync();

			return entities;
		}

	public async Task<T> Update(int id, T entity)
	{
		entity.Id = id;

		_context.Set<T>().Update(entity);
		await _context.SaveChangesAsync();

		return entity;
	}

	public IEnumerable<T> Get(Func<T, bool> predicate)
	{
		var results = _context.Set<T>().Where(predicate).AsEnumerable();

		return results;
	}
}
