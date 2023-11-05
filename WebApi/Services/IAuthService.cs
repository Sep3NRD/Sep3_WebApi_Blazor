using Domain.Models;

namespace WebAPI.Services;

public interface IAuthService
{
    Task RegisterUser(Costumer user);
    Task<Costumer> ValidateUser(string username, string password);
}