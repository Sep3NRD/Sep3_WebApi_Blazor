using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IOrderService
{
    Task CreateAsync(OrderCreationDto dto);
    Task ConfirmAsync(Order order);
}