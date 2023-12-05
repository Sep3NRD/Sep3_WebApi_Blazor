using Application.gRPCcon.Order;
using Application.LogicInterfaces;
using Domain.Models;

namespace Application.Logic;

public class OrderLogic: IOrderLogic
{

    private readonly IOrderGRPC iOrderGrpc;

    public OrderLogic(IOrderGRPC iOrderGrpc)
    {
        this.iOrderGrpc = iOrderGrpc;
    }

    public async Task<Order> CreateAsync(Order order)
    {
        if (order == null)
        {
            throw new Exception("Order is null");
        }

        Order created = await iOrderGrpc.CreateAsync(order);
        ConfirmAsync(order); // just a check if it works
        
        return created;
    }

    public async Task ConfirmAsync(Order order)
    {
        if (order == null)
        {
            throw new Exception("Order is null");
        }

         await iOrderGrpc.ConfirmAsync(order);
        
    }
}