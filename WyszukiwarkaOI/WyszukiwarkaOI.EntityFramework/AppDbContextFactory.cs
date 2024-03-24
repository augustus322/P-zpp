using Microsoft.EntityFrameworkCore;

namespace WyszukiwarkaOI.EntityFramework;
public class AppDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
{
	private readonly Action<DbContextOptionsBuilder> _configureDbContext = configureDbContext;

	public AppDbContext CreateDbContext()
	{
		var options = new DbContextOptionsBuilder<AppDbContext>();

		_configureDbContext(options);

		return new AppDbContext(options.Options);
	}
}
