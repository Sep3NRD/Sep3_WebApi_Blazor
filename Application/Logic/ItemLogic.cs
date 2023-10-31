using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class ItemLogic : IItemLogic
{
    
    private readonly IItemDao itemDao;
    public async Task<Item> CreateAsync(ItemCreationDto dto)
    {
        ValidateTodo(dto);
        Item item = new Item(dto.Name, dto.Price);
        Item created = await itemDao.CreateAsync(item);
        return created;
    }

    private void ValidateTodo(ItemCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Name))
            throw new Exception("Title cannot be empty.");
        if (dto.Price == null)
            throw new Exception("Title cannot be empty.");
    }
}