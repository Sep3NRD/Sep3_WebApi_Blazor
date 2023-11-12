using Domain.Models;

namespace WebAPI.Services;

public interface IAuthService
{
    Task RegisterUser(Customer user);
    Task<Customer> ValidateUser(string username, string password);
}