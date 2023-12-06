using Application.gRPCcon.Order;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class OrderLogic: IOrderLogic
{

    private readonly IOrderGRPC iOrderGrpc;

    public OrderLogic(IOrderGRPC iOrderGrpc)
    {
        this.iOrderGrpc = iOrderGrpc;
    }

    public async Task CreateAsync(CreateOrderDto dto)
    {
        if (dto == null)
        {
            throw new Exception("Order is null");
        }

        await iOrderGrpc.CreateAsync(dto);
        // ConfirmAsync(dto); // just a check if it works
        
    }

    public async Task ConfirmAsync(int orderId)
    {

        await iOrderGrpc.ConfirmAsync(orderId);
        
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await iOrderGrpc.GetAllAsync();
    }
}