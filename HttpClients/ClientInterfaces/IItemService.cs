using Domain.DTOs;

namespace HttpClients.ClientInterfaces;

public interface IItemService
{
    Task CreateAsync(ItemCreationDto dto);
}