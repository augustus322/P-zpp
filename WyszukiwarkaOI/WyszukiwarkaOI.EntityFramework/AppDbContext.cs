using Microsoft.EntityFrameworkCore;
using WyszukiwarkaOI.Domain.Models;

namespace WyszukiwarkaOI.EntityFramework;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
	public DbSet<Product> Products { get; set; }
}
