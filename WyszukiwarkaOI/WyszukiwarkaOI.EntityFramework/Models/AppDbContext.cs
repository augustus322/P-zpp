using Microsoft.EntityFrameworkCore;

namespace WyszukiwarkaOI.EntityFramework.Models;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}

public class Product(string shopLink, string name, decimal price, string shopName, string? description = null)
{
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public string? Description { get; set; } = description;
    public decimal Price { get; set; } = price;
    public string ShopName { get; set; } = shopName;
    public string ShopLink { get; set; } = shopLink;
}