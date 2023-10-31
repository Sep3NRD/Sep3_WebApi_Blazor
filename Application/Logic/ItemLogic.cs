using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class ItemLogic : IItemLogic
{
    public Task<Item> CreateAsync(ItemCreationDto dto)
    {
        throw new NotImplementedException();
    }
}