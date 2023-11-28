using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IItemService
{
    Task CreateAsync(ItemCreationDto dto);

    Task<ICollection<Item>> GetAsync(
        string? Name,
        double? Price);

    


}