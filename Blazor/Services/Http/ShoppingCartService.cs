using System.Text.Json;
using Blazor.Services.Interfaces;
using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.JSInterop;

namespace Blazor.Services.Http;

public class ShoppingCartService : IShoppingCartService
{
    private readonly ILocalStorageService localStorage;
    private readonly IToastService toastService;

    public List<Item> shoppingCart { get; set; } = new List<Item>();

    public ShoppingCartService(ILocalStorageService localStorage,IToastService toastService)
    {
        this.localStorage = localStorage;
        this.toastService = toastService;
    }
    
    
    public double CalculateTotal()
    {
        return shoppingCart.Sum(item => item.Price);
    }

    public async Task <List<Item>> GetAllItems()
    {
        var result = new List<Item>();
        var cart = await localStorage.GetItemAsync<List<Item>>("cart");
        if (cart==null)
        {
            return result;
        }

        foreach (var item in cart )
        {
            var cartItem = new Item
            {
                Category = item.Category,
                Description = item.Description,
                ItemId = item.ItemId,
                Name = item.Name,
                Price = item.Price,
                Stock = item.Stock
            };
            result.Add(cartItem);
        }

        return result;
    }

    public async Task AddItem(Item item)
    {
        shoppingCart.Add(item);
    }

    public void RemoveItem()
    {
        throw new NotImplementedException();
    }

    public event Action? onChange;
    public async Task AddToCart(Item item)
    {
        var cart = await localStorage.GetItemAsync<List<Item>>("cart");
        if (cart==null)
        {
            cart = new List<Item>();
        }
        cart.Add(item);
        await localStorage.SetItemAsync("Cart", cart);
    }
}