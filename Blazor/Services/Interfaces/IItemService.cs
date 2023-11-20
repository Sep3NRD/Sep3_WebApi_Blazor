using Domain.Models;

namespace Blazor.Services.Interfaces;

public interface IItemService
{
    Task CreateAsync(Item item);

    Task<ICollection<Item>> GetAsync();
}