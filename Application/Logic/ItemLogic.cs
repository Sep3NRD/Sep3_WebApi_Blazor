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
    public async Task<Item> CreateAsync(Item item)
    {
        ValidateItem(item);
        
        Item created = await itemGrpc.CreateAsync(item);
        return created;
    }

    // i will not use filters for now
    public async Task<IEnumerable<Item>> GetAsync()
    {
        return await itemGrpc.GetAsync();
    }

    public async Task<Item> GetByIdAsync(int id)
    {
        Item? item = await itemGrpc.GetByIdAsync(id);
        if (item == null)
        {
            throw new Exception($"Item with id {id} not found");
        }

        return new Item(item.Name,item.Description,item.Category, item.Price,item.Stock);
    }

    public async Task DeleteAsync(int id)
    {
        Item? item = await itemGrpc.GetByIdAsync(id);
        if (item == null)
        {
            throw new Exception($"Item with id {id} was not found!");
        }
        

        await itemGrpc.DeleteAsync(id);
    }


    private void ValidateItem(Item item)
    {
        if (string.IsNullOrEmpty(item.Name))
            throw new Exception("Title cannot be empty.");
        if (item.Price == 0)
            throw new Exception("Price cannot be zero.");
    }
}