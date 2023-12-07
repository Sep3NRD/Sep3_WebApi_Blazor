namespace Domain.DTOs;

public class AddToWishListDTO
{
    public int ItemId { get; set; }

    public int CustomerId { get; set; }

    public AddToWishListDTO(int itemId, int customerId)
    {
        ItemId = itemId;
        CustomerId = customerId;
    }
}