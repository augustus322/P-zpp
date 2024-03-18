using Microsoft.EntityFrameworkCore;

namespace WyszukiwarkaOI.EntityFramework;
public class AppDbContextFactory(string dbConnectionString)
{
	private readonly string _dbConnectionString = dbConnectionString;

	public AppDbContext CreateDbContext()
	{
		var options = new DbContextOptionsBuilder<AppDbContext>();
		options.UseSqlite(_dbConnectionString);

		return new AppDbContext(options.Options);
	}
}
