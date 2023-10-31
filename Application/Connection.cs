using Domain.Models;
using Grpc.Net.Client;

namespace Application;

public class Connection
{
    GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001");
    //object client = new Item.ItemClient(channel);
}