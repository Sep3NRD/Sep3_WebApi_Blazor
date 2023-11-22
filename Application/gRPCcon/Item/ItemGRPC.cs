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
      


       public Task<Domain.Models.Item> CreateAsync(Domain.Models.Item item)
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

       public Task<IEnumerable<Domain.Models.Item>> GetAsync(SearchItemParametersDto searchParameters)
       {
              throw new NotImplementedException();
       }
}