using Domain.Models;

namespace Domain.DTOs;

public class CreateOrderDto
{
    public string OrderDate { get; set; }
    public double totalPrice { get; set; }
    public List<Item> Items { get; init; } = new List<Item>();
    public string username { get; set; }
    public int addressId { get; set;}
}