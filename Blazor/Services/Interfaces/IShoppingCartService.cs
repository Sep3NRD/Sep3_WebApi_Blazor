using Domain.Models;

namespace Blazor.Services.Interfaces;

public interface IShoppingCartService
{
    public Task<List<Item>> GetAllItems();
    public Task DeleteItem(Item item);
    public Task Clear();


    event Action onChange;
    Task AddToCart(Item item);
}