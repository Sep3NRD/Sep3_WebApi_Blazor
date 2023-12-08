using Application.gRPCcon.Costumer;
using Domain.DTOs;
using Domain.Models;
using Google.Protobuf.Collections;
using Grpc.Net.Client;

namespace Application.gRPCcon.WishList;

public class WishListGRPC: IWIshListGRPC
{
    private ICustomerGRPC customerGrpc;
    
    
    public async Task AddToWishList(AddToWishListDTO dto)
    {   
        
        // Establish a gRPC channel to the server at the specified address
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
        // Create a client for the OrderService using the gRPC channel
        var client = new WishListService.WishListServiceClient(channel);

        var request = new WishListRequest()
        {
            Username = dto.Username,
            ItemId = dto.ItemId
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
            throw ex;
        }
    }

    public async Task<Domain.Models.WishList> GetWishListAsync(string username)
    {
        Console.WriteLine(username);
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
        var client = new WishListService.WishListServiceClient(channel);

        try
        {
            var request = new GetWishListRequest()
            {
                Username = username
            };
            var response = await client.getWishListAsync(request);
            
            var wishList = new Domain.Models.WishList
            {
                Items = ConvertItemPListToDomainModelList(response.Items)
            };
            
            return wishList;
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    private List<Domain.Models.Item> ConvertItemPListToDomainModelList(RepeatedField<ItemP> itemPList)
    {
        var itemList = new List<Domain.Models.Item>();

        foreach (var itemP in itemPList)
        {
            var item = new Domain.Models.Item
            {
                Name = itemP.Name,
                Description = itemP.Description,
                ItemId = itemP.ItemId,
                Price = itemP.Price,
                Category = itemP.Category,
                quantity = itemP.Quantity,
                Stock = itemP.Stock
            };

            itemList.Add(item);
        }

        return itemList;
    }
}