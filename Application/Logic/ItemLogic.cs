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
        
        ValidateTodo(dto);
        Item item = new Item(dto.Name, dto.Description, dto.Category, dto.Price , dto.Stock);
        Item created = await itemGrpc.CreateAsync(item);
        Console.WriteLine(">>>>>>>item logic ...."+item.Name);
        return created;
    }

    public async Task<IEnumerable<Item>> GetAsync(SearchItemParametersDto searchParameters)
    {
        return await itemGrpc.GetAsync(searchParameters);
    }

    private void ValidateTodo(ItemCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Name))
            throw new Exception("Title cannot be empty.");
        if (dto.Price == 0)
            throw new Exception("Title cannot be empty.");
    }
}