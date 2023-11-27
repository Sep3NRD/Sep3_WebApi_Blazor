namespace Domain.DTOs;

public class ItemUpdateDto
{
    public int ItemId { get; }
    public string Name { get; }
    public string Descrpition { get; }
    public string Category { get; }
    public double? Price { get; set; }
    public int? Stock { get; set; }

    public ItemUpdateDto(int itemId, string name, string descrpition, string category)
    {
        ItemId = itemId;
        Name = name;
        Descrpition = descrpition;
        Category = category;
    }
}