using System.Threading.Tasks;
using Grpc.Net.Client;
using Application;
using Domain.DTOs;
using Google.Protobuf;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace Application.gRPCcon.Item;

public class ItemGRPC : IItemGRPC
{
       public  Task<Domain.Models.Item> CreateAsync(Domain.Models.Item item)
       {
              // Establish a gRPC channel to the server at the specified address
              GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
              // Create a client for the ItemService using the gRPC channel
              var client = new ItemService.ItemServiceClient(channel);
              
              // Prepare the data to be sent to the gRPC service by mapping from the domain model to the gRPC model
              var itemToSend = new ItemP
              {
                     Name = item.Name,
                     Description = item.Description,
                     Category = item.Category,
                     Price = item.Price,
                     Stock = item.Stock
              };
              // Invoke the gRPC service method to post the item
               client.postItem(itemToSend);
               // Shutdown the gRPC channel once the operation is completed
               channel.ShutdownAsync();
               // Return a completed task with the original item as a result
              return Task.FromResult(item);
       }

       public async Task<IEnumerable<Domain.Models.Item>> GetAsync()
       {
              GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
              var client = new ItemService.ItemServiceClient(channel);
              // Prepare a gRPC request (in this case, an empty request)
              var request = new Google.Protobuf.WellKnownTypes.Empty();
              try
              {
                     // Call the gRPC service method to get a stream of items
                     var responseStream = client.getItems(request);
                     // Create a list to store the converted domain model items
                     var items = new List<Domain.Models.Item>();
                     
                     // Iterate over the response stream asynchronously and convert each item
                     await foreach (var itemP in responseStream.ResponseStream.ReadAllAsync())
                     {
                            var item = new Domain.Models.Item
                            {
                                   Name = itemP.Name,
                                   Description = itemP.Description,
                                   Category = itemP.Category,
                                   Price = itemP.Price,
                                   ItemId = itemP.ItemId,
                                   Stock = itemP.Stock
                            };
                            // Add the converted item to the list
                            items.Add(item);
                     }
                     // Return the list of domain model items
                     return items;
              }
              catch (RpcException ex)
              {
                     // Handle gRPC exceptions, log the error, and rethrow
                     Console.WriteLine($"gRPC error: {ex.Status}");
                     throw; 
              }
              finally
              {
                      channel.ShutdownAsync();
              }
              
       }

       

       public async Task<Domain.Models.Item> GetByIdAsync(int id)
       {
              GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
              var client = new ItemService.ItemServiceClient(channel);
              
              // Create a gRPC request with the specified item ID
              GetItemById idRequest = new GetItemById
              {
                     ItemId = id
              };
              try
              {
                     // Call the gRPC service method asynchronously to get an item by ID
                     ItemResponseP itemProto = await client.getItemByIdAsync(idRequest);
                     
                     // Check if the response is not null
                     if (itemProto != null)
                     {
                            Domain.Models.Item finalItem = new Domain.Models.Item
                            {
                                   ItemId = itemProto.Item.ItemId,
                                   Name = itemProto.Item.Name,
                                   Description = itemProto.Item.Description,
                                   Category = itemProto.Item.Category,
                                   Price = itemProto.Item.Price,
                                   Stock = itemProto.Item.Stock
                            };
                            // Return the item
                            return finalItem;
                     }
              }
              catch (Exception e)
              {
                     Console.WriteLine(e);
                     throw;
              }
              await channel.ShutdownAsync();
              return null;
       }

       public async Task DeleteAsync(int id)
       {
              try
              {
                     GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
                     var client = new ItemService.ItemServiceClient(channel);

                     // Call the gRPC service to delete the item
                     await client.deleteItemByIdAsync(new DeleteItemRequest() { ItemId = id });
              }
              catch (RpcException ex)
              {
                     // Handle gRPC service errors
                     Console.WriteLine($"Error deleting item from gRPC service: {ex.Status}");
                     throw;
              }
       }
       
       
       public async Task<UpdateItemDto> UpdateItemAsync(UpdateItemDto item)
       {
              try
              {
                     // Create a gRPC channel to the server at the specified address
                     // Note: The 'using' statement ensures proper cleanup of resources
                     using (var channel = GrpcChannel.ForAddress("http://localhost:9090"))
                     {
                            var client = new ItemService.ItemServiceClient(channel);
                            // Prepare a gRPC request to update an item
                            var itemToUpdate = new UpdateItemRequest
                            {
                                   ItemId = item.ItemId,
                                   Price = item.Price,
                                   Stock = item.Stock
                            };
                            
                            // Call the gRPC service method asynchronously to update the item
                            var response = await client.updateItemAsync(itemToUpdate);
                            // Return the original item after the update
                            return await Task.FromResult(item);
                     }
              }
              catch (RpcException ex)
              {
                     Console.WriteLine($"gRPC call failed: {ex}");
                     throw;
              }
       }
}