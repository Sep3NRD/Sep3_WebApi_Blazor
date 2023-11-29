namespace Application.gRPCcon.Order;

public interface IOrderGRPC
{
    Task<Domain.Models.Order> CreateAsync(Domain.Models.Order order);
}