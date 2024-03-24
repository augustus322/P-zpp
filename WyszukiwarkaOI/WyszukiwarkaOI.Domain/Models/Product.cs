namespace WyszukiwarkaOI.Domain.Models;

public class Product(string shopLink, string name, decimal price, string shopName, string? description = null)
	: DomainObject
{
	public string Name { get; set; } = name;
	public string? Description { get; set; } = description;
	public decimal Price { get; set; } = price;
	public string ShopName { get; set; } = shopName;
	public string ShopLink { get; set; } = shopLink;
}