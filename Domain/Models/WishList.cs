namespace Domain.Models;

public class WishList
{
    private int Id { get; set; }
    private Customer Customer { get; set; }
    public List<Item> Items { get; set; }

    public WishList()
    {
    }

}