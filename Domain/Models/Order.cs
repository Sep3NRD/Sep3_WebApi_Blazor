namespace Domain.Models;

public class Order
{
    public int Id { get; set; }
    public Customer Customer { get; init; } = new Customer();
    public List<Item> Items { get; init; } = new List<Item>();
    public Address Address { get; init; } = new Address();
    public string OrderDate { get; set; }
    public string DeliveryDate { get; set; }
    public bool IsConfirmed { get; set; }
    public double TotalPrice { get; set; }

    public Order()
    {
        
    }
}