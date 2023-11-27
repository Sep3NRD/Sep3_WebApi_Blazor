using Domain.DTOs;
using Domain.Models;

namespace Application.gRPCcon.Costumer;


public interface ICustomerGRPC
{
    Task<Domain.Models.Customer> CreateAsync(Domain.Models.Customer customer);
    Task<Domain.Models.Customer> ValidateLogin(string username, string password);
    Task<Customer> GetByUsernameAsync(CustomerLoginDto username);

}