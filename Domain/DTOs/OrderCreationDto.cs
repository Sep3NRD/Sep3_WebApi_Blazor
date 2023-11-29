using Domain.Models;

namespace Domain.DTOs;

public class OrderCreationDto
{
    
    public Customer Customer { get; init; } = new Customer();
    public List<Item> Items { get; init; } = new List<Item>();
    public Address Address { get; init; } = new Address();
    public string OrderDate { get; set; }
    public double totalPrice { get; set; }

    public OrderCreationDto()
    {
        
    }
}