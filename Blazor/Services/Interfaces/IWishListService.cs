using Domain.DTOs;

namespace Blazor.Services.Interfaces;

public interface IWishListService
{
    public Task AddToWishListAsync(AddToWishListDTO dto);
  
}