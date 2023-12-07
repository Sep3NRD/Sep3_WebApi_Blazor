using Application.gRPCcon.Costumer;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Application.Logic;

public class CustomerLogic : ICustomerLogic
{
   private readonly ICustomerGRPC iCustomerGrpc;

    // Constructor to inject ICustomerGRPC dependency
    public CustomerLogic(ICustomerGRPC iCustomerGrpc)
    {
        this.iCustomerGrpc = iCustomerGrpc;
    }

    // Method to create a new customer asynchronously
    public async Task<Customer> CreateAsync(Customer customer)
    {
        // Validate customer fields
        ValidateCustomer(customer);

        // Check for an existing user with the same username
        CustomerLoginDto dto = new CustomerLoginDto(customer.UserName, customer.Password);
        Customer toCreate = await GetByUsernameAsync(dto);

        if (toCreate != null)
        {
            throw new Exception("Username already exists");
        }

        // Create the customer after all checks
        Customer created = await iCustomerGrpc.CreateAsync(customer);
        return created;
    }

    // Method to validate customer login
    public async Task<Customer> LoginValidation(CustomerLoginDto userLoginDto)
    {
        Customer customer = await iCustomerGrpc.ValidateLogin(userLoginDto.Username.ToString(), userLoginDto.Password);

        if (customer == null)
        {
            throw new Exception("Username or password invalid");
        }

        return customer;
    }

    // Method to get a customer by username asynchronously
    public async Task<Customer> GetByUsernameAsync(CustomerLoginDto userLoginDto)
    {
        if (string.IsNullOrEmpty(userLoginDto.Username.ToString()))
        {
            throw new ArgumentException("Username cannot be null or empty.", nameof(userLoginDto.Username));
        }

        // Retrieve customer by username
        Customer customer = await iCustomerGrpc.GetByUsernameAsync(userLoginDto);
        return customer;
    }

    // Method to update customer information asynchronously
    public async Task UpdateAsync(Customer customer)
    {
        // Validate customer fields
        ValidateCustomer(customer);

        // Update customer information
        await iCustomerGrpc.UpdateAsync(customer);
    }

    public async Task AddNewAddress(AddNewAddressDTO dto)
    {
        Address address = new Address
        {
            Street = dto.Street,
            City = dto.City,
            DoorNumber = dto.DoorNumber,
            PostalCode = dto.PostalCode,
            Country = dto.Country,
            State = dto.State
        };
        ValidateAddress(address);

        await iCustomerGrpc.AddNewAddress(dto);
    }

    public async Task<ICollection<Address>> GetAddressesByUsername(string username)
    {
        CustomerLoginDto dto = new CustomerLoginDto()
        {
            Username = username
        };
        var customer = await iCustomerGrpc.GetByUsernameAsync(dto);

        if (customer!=null)
        {
            var addresses = await iCustomerGrpc.GetAddressesByUsername(username);
            return addresses;
        }
        else
        {
            throw new Exception("Something went wrong");
        }
        
    }


    private void ValidateCustomer(Customer customer)
{
    // Check if the customer object is null
    if (customer == null)
    {
        throw new ArgumentNullException(nameof(customer), "Customer cannot be null.");
    }

    // Check if the username is null or empty
    if (string.IsNullOrWhiteSpace(customer.UserName))
    {
        throw new ArgumentException("Username cannot be null or empty.", nameof(customer.UserName));
    }

    // Check if the password is null or empty
    if (string.IsNullOrWhiteSpace(customer.Password))
    {
        throw new ArgumentException("Password cannot be null or empty.", nameof(customer.Password));
    }

    // Check if the first name is null or empty
    if (string.IsNullOrWhiteSpace(customer.FirstName))
    {
        throw new ArgumentException("First name cannot be null or empty.", nameof(customer.FirstName));
    }

    // Check if the last name is null or empty
    if (string.IsNullOrWhiteSpace(customer.LastName))
    {
        throw new ArgumentException("Last name cannot be null or empty.", nameof(customer.LastName));
    }

    // Check if the address object is null
    if (customer.Address == null)
    {
        throw new ArgumentNullException(nameof(customer.Address), "Address cannot be null.");
    }

    // Validate the address object
    ValidateAddress(customer.Address);
}

private void ValidateAddress(Address address)
{
    // Check if the address object is null
    if (address == null)
    {
        throw new ArgumentNullException(nameof(address), "Address cannot be null.");
    }

    // Check if the door number is a positive integer
    if (address.DoorNumber <= 0)
    {
        throw new ArgumentException("Door number must be a positive integer.", nameof(address.DoorNumber));
    }

    // Check if the street in the address is null or empty
    if (string.IsNullOrWhiteSpace(address.Street))
    {
        throw new ArgumentException("Street in the address cannot be null or empty.", nameof(address.Street));
    }

    // Check if the city in the address is null or empty
    if (string.IsNullOrWhiteSpace(address.City))
    {
        throw new ArgumentException("City in the address cannot be null or empty.", nameof(address.City));
    }

    // Check if the state in the address is null or empty
    if (string.IsNullOrWhiteSpace(address.State))
    {
        throw new ArgumentException("State in the address cannot be null or empty.", nameof(address.State));
    }

    // Check if the postal code is a positive integer
    if (address.PostalCode <= 0)
    {
        throw new ArgumentException("Postal code must be a positive integer.", nameof(address.PostalCode));
    }

    // Check if the country in the address is null or empty
    if (string.IsNullOrWhiteSpace(address.Country))
    {
        throw new ArgumentException("Country in the address cannot be null or empty.", nameof(address.Country));
    }
}
}