using Microsoft.EntityFrameworkCore;

namespace WyszukiwarkaOI.EntityFramework;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
	public DbSet<Product> Products { get; set; }
}

// TODO: Move that class to some kind of 'Models' folder in 'Domain' project or sth
public class Product(string shopLink, string name, decimal price, string shopName, string? description = null)
{
	public int Id { get; set; }
	public string Name { get; set; } = name;
	public string? Description { get; set; } = description;
	public decimal Price { get; set; } = price;
	public string ShopName { get; set; } = shopName;
	public string ShopLink { get; set; } = shopLink;
}