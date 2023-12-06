using Domain.DTOs;
using Domain.Models;
namespace Application.gRPCcon.Order;

public interface IOrderGRPC
{
    Task CreateAsync(CreateOrderDto dto);
    Task ConfirmAsync(int orderId);
    Task<IEnumerable<Domain.Models.Order>> GetAllAsync();
}