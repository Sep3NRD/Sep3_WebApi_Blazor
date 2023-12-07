using Domain.DTOs;
using Grpc.Net.Client;

namespace Application.gRPCcon.WishList;

public class WishListGRPC: IWIshListGRPC
{
    public async Task AddToWishList(AddToWishListDTO dto)
    {   
        
        // Establish a gRPC channel to the server at the specified address
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
        // Create a client for the OrderService using the gRPC channel
        var client = new WishListService.WishListServiceClient(channel);

        var request = new WishListRequest()
        {
            ItemId = dto.ItemId,
            CustomerId = dto.CustomerId
        };
        
        try
        {
            // Call the gRPC service method
            var response = await client.addToWishListAsync(request);
            await  channel.ShutdownAsync();
        }
        catch (Exception ex)
        {
            // Handle exceptions
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}