namespace Application.gRPCcon.Costumer;


public interface ICostumerGrpc
{
    Task<Domain.Models.Customer> CreateAsync(Domain.Models.Customer customer);
    Task<Domain.Models.Customer> GetAsync(string username, string password);

}