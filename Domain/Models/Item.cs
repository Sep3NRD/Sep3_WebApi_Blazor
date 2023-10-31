namespace Domain.Models;

public class Item
{
    public int ItemId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set;}
    public string? Category { get; set; }
    public double? Price { get; set; }
    public int? Stock { get; set; }
}