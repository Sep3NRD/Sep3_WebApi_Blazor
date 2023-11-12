namespace Application.gRPCcon.Costumer;


public interface ICostumerGrpc
{
    Task<Domain.Models.Costumer> createAsync(Domain.Models.Costumer costumer);
    Task<Domain.Models.Costumer> getAsync(String username, string password);

}