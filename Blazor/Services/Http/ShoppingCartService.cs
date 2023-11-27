using System.Text.Json;
using Blazor.Services.Interfaces;
using Domain.Models;
using Microsoft.JSInterop;

namespace Blazor.Services.Http;

public class ShoppingCartService : IShoppingCartService
{
    private const string CartKey = "shoppingCart";
    private IJSRuntime _jsRuntime;
    public List<Item> shoppingCart { get; set; } = new List<Item>()
    {
        new Item("Nvidia","verynice","GPU",100.00,5),
        new Item("AMD", "awesome", "CPU", 150.00, 3)
    };

    public ShoppingCartService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    
    public double CalculateTotal()
    {
        return shoppingCart.Sum(item => item.Price);
    }

    public List<Item> GetAllItems()
    {
        return shoppingCart;
    }

    public void AddItem()
    {
        throw new NotImplementedException();
    }

    public void RemoveItem()
    {
        throw new NotImplementedException();
    }
    
    public async Task LoadCartFromLocalStorage()
    {
        var cartJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", CartKey);

        if (!string.IsNullOrEmpty(cartJson))
        {
            shoppingCart = JsonSerializer.Deserialize<List<Item>>(cartJson);
        }
    }

    public async Task SaveCartToLocalStorage()
    {
        var cartJson = JsonSerializer.Serialize(shoppingCart);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", CartKey, cartJson);
    }
}