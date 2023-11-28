namespace Domain.DTOs;

public class SearchItemParametersDto
{
    public string? Name {get;}
    public double Price { get; }
    
    public int ItemId { get; }

    public SearchItemParametersDto(string? name, double price)
    {
        Name = name;
        Price = price;
    }

    public SearchItemParametersDto(int itemId)
    {
        ItemId = itemId;
    }
}