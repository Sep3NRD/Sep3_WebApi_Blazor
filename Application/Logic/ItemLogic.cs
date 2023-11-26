using Application.gRPCcon.Item;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class ItemLogic : IItemLogic
{
    
    private readonly IItemGRPC itemGrpc;

    public ItemLogic(IItemGRPC itemGrpc)
    {
        this.itemGrpc = itemGrpc;
    }
    public async Task<Item> CreateAsync(ItemCreationDto dto)
    {
        
        ValidateItem(dto);
        Item item = new Item(dto.Name, dto.Description, dto.Category, dto.Price , dto.Stock);
        Item created = await itemGrpc.CreateAsync(item);
        return created;
    }

    // i will not use filters for now
    public async Task<IEnumerable<Item>> GetAsync()
    {
        return await itemGrpc.GetAsync();
    }

    private void ValidateItem(ItemCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Name))
            throw new Exception("Title cannot be empty.");
        if (dto.Price == 0)
            throw new Exception("Title cannot be empty.");
    }
}