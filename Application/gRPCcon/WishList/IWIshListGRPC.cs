using Domain.DTOs;

namespace Application.gRPCcon.WishList;

public interface IWIshListGRPC
{
    Task AddToWishList(AddToWishListDTO dto);
}