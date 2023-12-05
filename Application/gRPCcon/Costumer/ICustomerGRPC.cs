using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.gRPCcon.Costumer;


public interface ICustomerGRPC
{
    Task<Domain.Models.Customer> CreateAsync(Domain.Models.Customer customer);
    Task<Domain.Models.Customer> ValidateLogin(string username, string password);
    Task<Customer> GetByUsernameAsync(CustomerLoginDto username);
    Task UpdateAsync(Customer customer);
    Task AddNewAddress(AddNewAddressDTO dto);

}