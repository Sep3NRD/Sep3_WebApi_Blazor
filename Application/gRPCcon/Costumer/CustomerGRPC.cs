using Grpc.Net.Client;


namespace Application.gRPCcon.Costumer;

public class CustomerGRPC : ICostumerGrpc
{
    public async Task<Domain.Models.Customer>  CreateAsync(Domain.Models.Customer customer)
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

    public Task<Domain.Models.Customer> GetAsync(string username, string password)
    {
        throw new NotImplementedException();
    }
}