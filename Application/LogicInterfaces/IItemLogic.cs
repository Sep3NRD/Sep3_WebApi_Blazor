using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IItemLogic
{
    Task<Domain.Models.Item> CreateAsync(Item item);
    Task<IEnumerable<Item>> GetAsync();

    Task<Domain.Models.Item> GetByIdAsync(int id);
}