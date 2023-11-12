using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface ICustomerLogic
{
    Task<Customer> CreateAsync(Customer customer);
    Task<IEnumerable<Customer>> GetAsync(SearchUserParametersDto searchParameters);
    
}