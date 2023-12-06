
using Blazor.Services.Interfaces;
using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Domain.Models;

namespace Blazor.Services.Http
{
    // Service responsible for managing the shopping cart functionality.
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ILocalStorageService localStorage; // Service for interacting with local storage.
        private readonly IToastService toastService; // Service for displaying toast notifications.

        // Constructor to initialize the ShoppingCartService with required services.
        public ShoppingCartService(ILocalStorageService localStorage, IToastService toastService)
        {
            this.localStorage = localStorage;
            this.toastService = toastService;
        }

        // Retrieves all items in the shopping cart.
        public async Task<List<Item>> GetAllItems()
        {
            var result = new List<Item>();
            var cart = await localStorage.GetItemAsync<List<Item>>("cart");

            // If the cart is empty, return an empty list.
            if (cart == null)
            {
                return result;
            }

            // Create new Item instances  and return the list of items.
            foreach (var item in cart)
            {
                var cartItem = new Item
                {
                    Category = item.Category,
                    Description = item.Description,
                    ItemId = item.ItemId,
                    Name = item.Name,
                    Price = item.Price,
                    Stock = item.Stock,
                    quantity = item.quantity
                };
                result.Add(cartItem);
            }

            return result;
        }

        // Deletes a specified item from the shopping cart.
        public async Task DeleteItem(Item item)
        {
            var cart = await localStorage.GetItemAsync<List<Item>>("cart");

            // If the cart is empty, return without making any changes.
            if (cart == null)
            {
                return;
            }

            // Find and remove the specified item from the cart, then update local storage.
            var cartItem = cart.Find(c => c.ItemId == item.ItemId);
            cart.Remove(cartItem);
            await localStorage.SetItemAsync("cart", cart);
        }

        // Clears the entire shopping cart.
        public async Task Clear()
        {
            await localStorage.ClearAsync();
        }

        // Adds a specified item to the shopping cart.
        public async Task AddToCart(Item item)
        {
            // Retrieve the current cart from local storage
            var cart = await localStorage.GetItemAsync<List<Item>>("cart");

            // If the cart is empty, initialize it as a new list.
            if (cart == null)
            {
                cart = new List<Item>();
            }

            // Check if the item is already in the cart
            var existingItem = cart.FirstOrDefault(i => i.ItemId == item.ItemId);

            if (existingItem != null)
            {
                // If the item is already in the cart, increment its quantity
                existingItem.quantity++;

            }
            else
            {
                // If the item is not in the cart, add it to the cart
                item.quantity = 1; // Assuming you have a "Quantity" property in your Item class
                cart.Add(item);
            }

            // Update the cart in local storage
            await localStorage.SetItemAsync("cart", cart);
            
        }
    }
}


