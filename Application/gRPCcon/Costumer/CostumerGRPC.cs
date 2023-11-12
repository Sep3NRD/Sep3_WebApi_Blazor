using Grpc.Net.Client;

namespace Application.gRPCcon.Costumer;

public class CostumerGRPC : ICostumerGrpc
{
    public Task<Domain.Models.Costumer> createAsync(Domain.Models.Costumer costumer)
    {
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
        var client = new CostumerS
        
        return null;

    }

    public Task<Domain.Models.Costumer> getAsync(string username, string password)
    {
        throw new NotImplementedException();
    }
}