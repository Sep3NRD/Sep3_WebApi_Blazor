namespace Domain.DTOs;

public class ItemCreationDto
{
    public string Name { get; set; }
    public string? Description { get; set;}
    public string? Category { get; set; }
    public double Price { get; set; }
    public int? Stock { get; set; }

    public ItemCreationDto(string name, string? description, string? category, double price, int? stock)
    {
        Name = name;
        Description = description;
        Category = category;
        Price = price;
        Stock = stock;
    }
}