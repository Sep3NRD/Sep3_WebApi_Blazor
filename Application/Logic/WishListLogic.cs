using Application.gRPCcon.WishList;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class WishListLogic: IWishListLogic
{
    private readonly IWIshListGRPC iWishGrpc;

    public WishListLogic(IWIshListGRPC iWishGrpc)
    {
        this.iWishGrpc = iWishGrpc;
    }
    
    public async Task AddToWishListAsync(AddToWishListDTO dto)
    {
        if (dto == null)
        {
            throw new Exception("Order is null");
        }

        await iWishGrpc.AddToWishList(dto);
    }

    public async Task<WishList> GetWishListAsync(string usernamed)
    {
        if (usernamed ==null)
        {
            throw new Exception("Customer id is null");
        }

        return await iWishGrpc.GetWishListAsync(usernamed);
    }
}