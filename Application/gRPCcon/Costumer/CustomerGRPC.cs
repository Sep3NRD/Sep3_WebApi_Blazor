using System.Net;
using Domain.DTOs;
using Domain.Models;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace Application.gRPCcon.Costumer;

public class CustomerGRPC : ICustomerGRPC
{
    // Method to create a customer asynchronously
    public async Task<Customer> CreateAsync(Customer customer)
    {
        // Create a gRPC channel for communication
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
        var client = new CustomerService.CustomerServiceClient(channel);

        // Convert the customer's address to the gRPC address format
        var customerAddressToSend = new AddressP
        {
            DoorNumber = customer.Address.DoorNumber,
            Street = customer.Address.Street,
            City = customer.Address.City,
            State = customer.Address.State,
            PostalCode = customer.Address.PostalCode,
            Country = customer.Address.Country
        };

        // Convert the customer to the gRPC customer format
        var customerToCreate = new CustomerP
        {
            Username = customer.UserName,
            Password = customer.Password,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Address = customerAddressToSend,
            Role = customer.Role
        };

        // Call the gRPC service to create a customer
        var response = await client.createCustomerAsync(customerToCreate);

        // Shutdown the gRPC channel
        channel.ShutdownAsync();

        // Set the customer ID from the response and return the customer
        customer.Id = response.Customer.Id;
        return customer;
    }

    // Method to validate customer login
    public async Task<Customer> ValidateLogin(string username, string password)
    {
        // Create a gRPC channel for communication
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
        var client = new CustomerService.CustomerServiceClient(channel);

        // Create a gRPC request for customer login validation
        GetCustomerRequest customerRequest = new GetCustomerRequest
        {
            Username = username,
            Password = password
        };

        // Call the gRPC service to validate customer login
        CustomerResponseP customerProto = await client.getCustomerAsync(customerRequest);

        // Convert the gRPC customer to the application's customer format
        Address addressForFinalCustomer = new Address
        {
            DoorNumber = customerProto.Customer.Address.DoorNumber,
            Street = customerProto.Customer.Address.Street,
            City = customerProto.Customer.Address.City,
            State = customerProto.Customer.Address.State,
            PostalCode = customerProto.Customer.Address.PostalCode,
            Country = customerProto.Customer.Address.Country
        };

        Customer finalCustomer = new Customer
        {
            UserName = customerProto.Customer.Username,
            Password = customerProto.Customer.Password,
            FirstName = customerProto.Customer.FirstName,
            LastName = customerProto.Customer.LastName,
            Address = addressForFinalCustomer,
            Role = customerProto.Customer.Role
        };

        // Shutdown the gRPC channel
        await channel.ShutdownAsync();

        // Return the validated customer
        return finalCustomer;
    }

    // Method to get a customer by username asynchronously
    public async Task<Customer> GetByUsernameAsync(CustomerLoginDto username)
    {
        // Create a gRPC channel for communication
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
        var client = new CustomerService.CustomerServiceClient(channel);

        // Create a gRPC request for getting a customer by username
        GetCustomerByUsername usernameRequest = new GetCustomerByUsername
        {
            Username = username.Username
        };

        try
        {
            // Call the gRPC service to get a customer by username
            CustomerResponseP customerProto = await client.getCustomerByUsernameAsync(usernameRequest);

            // If the customer exists, convert it to the application's customer format
            if (customerProto != null)
            {
                Address addressForFinalCustomer = new Address
                {
                    DoorNumber = customerProto.Customer.Address.DoorNumber,
                    Street = customerProto.Customer.Address.Street,
                    City = customerProto.Customer.Address.City,
                    State = customerProto.Customer.Address.State,
                    PostalCode = customerProto.Customer.Address.PostalCode,
                    Country = customerProto.Customer.Address.Country,
                    id = customerProto.Customer.Address.Id
                };

                Customer finalCustomer = new Customer
                {
                    Id = customerProto.Customer.Id,
                    UserName = customerProto.Customer.Username,
                    Password = customerProto.Customer.Password,
                    FirstName = customerProto.Customer.FirstName,
                    LastName = customerProto.Customer.LastName,
                    Address = addressForFinalCustomer,
                    Role = customerProto.Customer.Role
                };

                // Return the retrieved customer
                return finalCustomer;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        // Shutdown the gRPC channel
        await channel.ShutdownAsync();

        // Return null if the customer does not exist
        return null;
    }

    // Method to update customer information asynchronously
    public async Task UpdateAsync(Customer customer)
    {
        // Create a gRPC channel for communication
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
        var client = new CustomerService.CustomerServiceClient(channel);

        // Convert the customer's address to the gRPC address format
        var customerAddressToSend = new AddressP
        {
            DoorNumber = customer.Address.DoorNumber,
            Street = customer.Address.Street,
            City = customer.Address.City,
            State = customer.Address.State,
            PostalCode = customer.Address.PostalCode,
            Country = customer.Address.Country
        };

        // Convert the customer to the gRPC customer format
        var customerToCreate = new CustomerP
        {
            Id = customer.Id,
            Username = customer.UserName,
            Password = customer.Password,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Address = customerAddressToSend,
            Role = customer.Role
        };

        // Call the gRPC service to update customer information
        await client.updateCustomerAsync(customerToCreate);

        // Shutdown the gRPC channel
        await channel.ShutdownAsync();
    }

    public async Task AddNewAddress(AddNewAddressDTO dto)
    {
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
        var client = new CustomerService.CustomerServiceClient(channel);

        // Convert the customer's address to the gRPC address format
        var newAddress = new NewAddressP()
        {
            DoorNumber = dto.DoorNumber,
            Street = dto.Street,
            City = dto.City,
            State = dto.State,
            PostalCode = dto.PostalCode,
            Country = dto.Country,
            Username = dto.username
        };
        await client.addNewAddressAsync(newAddress);
        await channel.ShutdownAsync();
    }
}