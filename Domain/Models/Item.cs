namespace Domain.Models;

public class Item
{
    public int ItemId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set;}
    public string? Category { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }

    public Item(string name, double price)
    {
        Name = name;
        Price = price;
    }

    public Item(string name, string? description, string? category, double price, int stock)
    {
        Name = name;
        Description = description;
        Category = category;
        Price = price;
        Stock = stock;
    }
}