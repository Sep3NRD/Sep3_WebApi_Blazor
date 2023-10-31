using Domain.DTOs;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    Task RegisterUserAsync(UserLoginDto dto);

}