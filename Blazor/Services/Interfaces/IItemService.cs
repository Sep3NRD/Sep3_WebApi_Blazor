using Domain.DTOs;
using Domain.Models;

namespace Blazor.Services.Interfaces;

public interface IItemService
{
    Task CreateAsync(Item item);

    Task<ICollection<Item>> GetAsync();
    Task<Item> GetItemById(int id);
    Task UpdateItem(UpdateItemDto item);
    Task DeleteItem(int itemId);
}
