using Domain.DTOs;

namespace HttpClients.ClientInterfaces;

public interface IOrderService
{
    Task CreateAsync(OrderCreationDto dto);
}