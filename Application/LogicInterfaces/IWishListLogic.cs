using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface IWishListLogic
{
    Task AddToWishListAsync(AddToWishListDTO dto);
}