using Domain.DTOs;

namespace Application.gRPCcon.Order;

public interface IOrderGRPC
{
    Task CreateAsync(CreateOrderDto dto);
    Task ConfirmAsync(Domain.Models.Order order);
}