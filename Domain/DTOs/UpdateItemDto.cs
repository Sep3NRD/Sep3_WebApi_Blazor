namespace Domain.DTOs;
public class UpdateItemDto
{
    public int ItemId { get;}
    public double Price { get; set; }
    public int Stock { get; set; }
    public UpdateItemDto(int itemId, double price, int stock)
    {
        ItemId = itemId;
        Price = price;
        Stock = stock;
    }
    public UpdateItemDto()
    {
        
    }
    
    
}