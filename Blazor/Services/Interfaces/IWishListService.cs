using Domain.DTOs;
using Domain.Models;

namespace Blazor.Services.Interfaces;

public interface IWishListService
{
    public Task AddToWishListAsync(AddToWishListDTO dto);
    Task<WishList> GetWishListAsync(string username);
}