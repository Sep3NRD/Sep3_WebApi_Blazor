using Domain.Models;

namespace Blazor.Services.Interfaces;

public interface IShoppingCartService
{
    public double CalculateTotal();
    public Task<List<Item>> GetAllItems();
    public Task AddItem(Item item);
    public void RemoveItem();


    event Action onChange;
    Task AddToCart(Item item);
}