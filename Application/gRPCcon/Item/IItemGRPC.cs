namespace Application.gRPCcon.Item;

public interface IItemGRPC
{
    Task<Domain.Models.Item> CreateAsync(Domain.Models.Item item);
}