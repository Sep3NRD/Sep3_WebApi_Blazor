using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface ICustomerLogic
{
    Task<Customer> CreateAsync(Customer customer);
    Task<Customer> LoginValidation(CustomerLoginDto userLoginDto);
    Task<Customer> GetByUsernameAsync(CustomerLoginDto userLoginDto);
    
}