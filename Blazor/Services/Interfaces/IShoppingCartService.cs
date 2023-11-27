using Domain.Models;

namespace Blazor.Services.Interfaces;

public interface IShoppingCartService
{
    public double CalculateTotal();
    public List<Item> GetAllItems();
    public void AddItem();
    public void RemoveItem();

    public Task LoadCartFromLocalStorage();
    public Task SaveCartToLocalStorage();
}