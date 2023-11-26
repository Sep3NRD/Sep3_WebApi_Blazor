using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IItemLogic
{
    Task<Domain.Models.Item> CreateAsync(ItemCreationDto dto);
    Task<IEnumerable<Item>> GetAsync();
}