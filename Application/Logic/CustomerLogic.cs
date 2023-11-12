using Application.gRPCcon.Costumer;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class CustomerLogic : ICustomerLogic
{
    private readonly CustomerGRPC customerGrpc;
    public CustomerLogic(CustomerGRPC customerGrpc)
    {
        this.customerGrpc = customerGrpc;
    }
    public async Task<Customer> CreateAsync(Customer customer)
    {
        ValidateCustomer(customer); // validates customer fields

        Customer created = await customerGrpc.CreateAsync(customer);
        return created;
    }

    public Task<IEnumerable<Customer>> GetAsync(SearchUserParametersDto searchParameters)
    {
        throw new NotImplementedException();
    }

    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    private void ValidateCustomer(Customer customer)
{
    if (customer == null)
    {
        throw new ArgumentNullException(nameof(customer), "Customer cannot be null.");
    }

    if (string.IsNullOrWhiteSpace(customer.UserName))
    {
        throw new ArgumentException("Username cannot be null or empty.", nameof(customer.UserName));
    }

    if (string.IsNullOrWhiteSpace(customer.Password))
    {
        throw new ArgumentException("Password cannot be null or empty.", nameof(customer.Password));
    }

    if (string.IsNullOrWhiteSpace(customer.FirstName))
    {
        throw new ArgumentException("First name cannot be null or empty.", nameof(customer.FirstName));
    }

    if (string.IsNullOrWhiteSpace(customer.LastName))
    {
        throw new ArgumentException("Last name cannot be null or empty.", nameof(customer.LastName));
    }

    if (customer.Address == null)
    {
        throw new ArgumentNullException(nameof(customer.Address), "Address cannot be null.");
    }

    ValidateAddress(customer.Address);
    
}

private void ValidateAddress(Address address)
{
    if (address == null)
    {
        throw new ArgumentNullException(nameof(address), "Address cannot be null.");
    }

    if (address.DoorNumber <= 0)
    {
        throw new ArgumentException("Door number must be a positive integer.", nameof(address.DoorNumber));
    }

    if (string.IsNullOrWhiteSpace(address.Street))
    {
        throw new ArgumentException("Street in the address cannot be null or empty.", nameof(address.Street));
    }

    if (string.IsNullOrWhiteSpace(address.City))
    {
        throw new ArgumentException("City in the address cannot be null or empty.", nameof(address.City));
    }

    if (string.IsNullOrWhiteSpace(address.State))
    {
        throw new ArgumentException("State in the address cannot be null or empty.", nameof(address.State));
    }

    if (address.PostalCode <= 0)
    {
        throw new ArgumentException("Postal code must be a positive integer.", nameof(address.PostalCode));
    }

    if (string.IsNullOrWhiteSpace(address.Country))
    {
        throw new ArgumentException("Country in the address cannot be null or empty.", nameof(address.Country));
    }
    
}
}