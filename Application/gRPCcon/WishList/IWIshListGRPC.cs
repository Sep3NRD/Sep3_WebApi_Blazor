using Domain.DTOs;

namespace Application.gRPCcon.WishList;

public interface IWIshListGRPC
{
    Task AddToWishList(AddToWishListDTO dto);
    Task<Domain.Models.WishList> GetWishListAsync(string username);
}