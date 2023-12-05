using Domain.DTOs;
using Domain.Models;

namespace Blazor.Services;

public interface ICustomerService
{
    Task CreateAsync(Customer customer);
    Task<Customer> GetAsync(CustomerLoginDto userLoginDto);
    Task<Customer> GetByUsernameAsync(CustomerLoginDto userLoginDto);
    Task UpdateCustomerAsync(Customer customer);
    Task AddNewAddress(AddNewAddressDTO dto);
}