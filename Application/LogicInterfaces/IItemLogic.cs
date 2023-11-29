using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IItemLogic
{
    Task<Domain.Models.Item> CreateAsync(Item item);
    Task<IEnumerable<Item>> GetAsync();

    Task<Item> GetByIdAsync(int id);
    Task DeleteAsync(int id);
    Task<UpdateItemDto> UpdateItemAsync(UpdateItemDto item);
}