using Application.gRPCcon.WishList;
using Application.LogicInterfaces;
using Domain.DTOs;

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
}