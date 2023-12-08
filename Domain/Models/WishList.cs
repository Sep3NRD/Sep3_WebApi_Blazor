namespace Domain.Models;

public class WishList
{
    public int Id { get; set; }
    public Customer Customer { get; set; }
    public List<Item> Items { get; set; }

    public WishList()
    {
    }

}