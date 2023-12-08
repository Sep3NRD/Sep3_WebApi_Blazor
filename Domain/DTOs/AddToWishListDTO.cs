namespace Domain.DTOs;

public class AddToWishListDTO
{
    public int ItemId { get; set; }

    public string Username { get; set; }

    public AddToWishListDTO(int itemId, string username)
    {
        ItemId = itemId;
        Username = username;
    }
}