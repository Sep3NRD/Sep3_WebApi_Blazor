using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IItemLogic
{
    Task<Item> CreateAsync(ItemCreationDto dto);
}