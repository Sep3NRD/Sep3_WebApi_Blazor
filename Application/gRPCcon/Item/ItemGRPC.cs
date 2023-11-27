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
              GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
              var client = new ItemService.ItemServiceClient(channel);

              var itemToSend = new ItemP
              {
                     Name = item.Name,
                     Description = item.Description,
                     Category = item.Category,
                     Price = item.Price,
                     Stock = item.Stock
              };
               client.postItem(itemToSend);
               channel.ShutdownAsync();
              return Task.FromResult(item);
       }

       public async Task<IEnumerable<Domain.Models.Item>> GetAsync()
       {
              GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
              var client = new ItemService.ItemServiceClient(channel);
              var request = new Google.Protobuf.WellKnownTypes.Empty();
              try
              {
                     var responseStream = client.getItems(request);

                     // Process the streaming response
                     var items = new List<Domain.Models.Item>();

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

                            items.Add(item);
                     }

                     return items;
              }
              catch (RpcException ex)
              {
                     Console.WriteLine($"gRPC error: {ex.Status}");
                     throw; 
              }
              finally
              {
                      channel.ShutdownAsync();
              }
              
       }
}