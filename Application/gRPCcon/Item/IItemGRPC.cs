using Domain.DTOs;

namespace Application.gRPCcon.Item;

public interface IItemGRPC
{
    Task<Domain.Models.Item> CreateAsync(Domain.Models.Item item);
    
    Task<IEnumerable<Domain.Models.Item>> GetAsync();
    Task<Domain.Models.Item> GetByIdAsync(int id);
    
    Task DeleteAsync(int id);
}