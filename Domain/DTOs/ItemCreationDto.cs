namespace Domain.DTOs;

public class ItemCreationDto
{
    public string Name { get; set; }
    public double Price { get; set; }

    public ItemCreationDto(string name, double price)
    {
        Name = name;
        Price = price;
    }
}