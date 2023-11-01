using System.Threading.Tasks;
using Grpc.Net.Client;
using Application;
using Google.Protobuf;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace Application.gRPCcon.Item;

public class ItemGRPC : IItemGRPC
{
      


       public Task<Domain.Models.Item> CreateAsync(Domain.Models.Item item)
       {
              GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:9090");
              var client = new ItemService.ItemServiceClient(channel);

              var itemToSend = new ItemP
              {
                     Name = item.Name,
                     Price = (double)item.Price
              };
              
              client.postItem(itemToSend);
             return Task.FromResult(item);
       }
}