using Domain.Models;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;


namespace Application.gRPCcon.Costumer;

public class CustomerGRPC: ICustomerGRPC
{
    public async Task<Customer>  CreateAsync(Customer customer)
    {
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
        var client = new CustomerService.CustomerServiceClient(channel);

        var customerAdressToSend = new AddressP
        {
            DoorNumber = customer.Address.DoorNumber,
            Street = customer.Address.Street,
            City = customer.Address.City,
            State = customer.Address.State,
            PostalCode = customer.Address.PostalCode,
            Country = customer.Address.Country
        };
        
        var customerToCreate = new CustomerP
        {
            Username = customer.UserName,
            Password = customer.Password,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Address = customerAdressToSend
        };

        var response = await client.createCustomerAsync(customerToCreate);
        channel.ShutdownAsync();

        return customer;

    }

    public async Task<Customer> ValidateLogin(string username, string password)
    {
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
        var client = new CustomerService.CustomerServiceClient(channel);

        GetCustomerRequest customerRequest = new GetCustomerRequest
        {
            Username = username,
            Password = password
        };

        CustomerResponseP customerProto = await client.getCustomerAsync(customerRequest);
        
        
        Address addressForFinalCostumer = new Address
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
            Address = addressForFinalCostumer
        };
       await  channel.ShutdownAsync();
        return finalCustomer;
    }

    public async  Task<Customer> GetByUsernameAsync(string username)
    {
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
        var client = new CustomerService.CustomerServiceClient(channel);

        GetCustomerByUsername usernameRequest = new GetCustomerByUsername
        {
            Username = username
        };
        
        CustomerResponseP customerProto =  await client.getCustomerByUsernameAsync(usernameRequest);

        Address addressForFinalCostumer = new Address
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
            Address = addressForFinalCostumer
        };

        await channel.ShutdownAsync();

        return finalCustomer;
    }
}