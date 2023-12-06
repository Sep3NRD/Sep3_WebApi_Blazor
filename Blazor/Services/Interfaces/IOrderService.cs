using Domain.DTOs;
using Domain.Models;

namespace Blazor.Services.Interfaces;

public interface IOrderService
{
    Task CreateAsync(CreateOrderDto dto);
    Task<ICollection<Order>> GetAllAsync();
    Task ConfirmAsync(int orderId);
}