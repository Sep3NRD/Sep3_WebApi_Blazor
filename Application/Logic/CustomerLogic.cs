using Application.gRPCcon.Costumer;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class CustomerLogic : ICustomerLogic
{
    private readonly ICustomerGRPC iCustomerGrpc;
    public CustomerLogic(ICustomerGRPC iCustomerGrpc)
    {
        this.iCustomerGrpc = iCustomerGrpc;
    }
    public async Task<Customer> CreateAsync(Customer customer)
    {
        ValidateCustomer(customer); // validates customer fields

        
        //  Checks for an existing user with the same username
        CustomerLoginDto dto = new CustomerLoginDto(customer.UserName, customer.Password);
        
        Customer tocreate = await GetByUsernameAsync(dto);
        if (tocreate!=null)
        {
            throw new Exception("Username already exists");
        }

        Customer created = await iCustomerGrpc.CreateAsync(customer);  // Creates the customer after all checks
        return created;
    }

    public async Task<Customer> LoginValidation(CustomerLoginDto userLoginDto)
    {
        Customer customer = await iCustomerGrpc.ValidateLogin(userLoginDto.Username, userLoginDto.Password);
        if (customer==null)
        {
            throw new Exception("Username or password invalid");
        }

        return customer;
    }
    

    public async Task<Customer> GetByUsernameAsync(CustomerLoginDto userLoginDto)
    {
        if (string.IsNullOrEmpty(userLoginDto.Username))
        {
            throw new ArgumentException("Username cannot be null or empty.", nameof(userLoginDto.Username));
        }
        Customer customer = await iCustomerGrpc.GetByUsernameAsync(userLoginDto.Username);

        return customer;
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