using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IWishListLogic
{
    Task AddToWishListAsync(AddToWishListDTO dto);
    Task<WishList> GetWishListAsync(string username);
}