using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface ICustomerLogic
{
    Task<Customer> CreateAsync(Customer customer);
    Task<Customer> GetAsync(UserLoginDto userLoginDto);
    Task<Customer> GetByUsernameAsync(UserLoginDto userLoginDto);
    
}