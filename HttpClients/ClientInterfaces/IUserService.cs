using Domain.DTOs;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    Task RegisterUserAsync(CustomerLoginDto dto);

}