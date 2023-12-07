using Domain.DTOs;
using Domain.Models;
using Google.Protobuf.Collections;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Application.gRPCcon.Order;

public class OrderGRPC: IOrderGRPC
{
    public async Task CreateAsync(CreateOrderDto dto)
    {
        // Establish a gRPC channel to the server at the specified address
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
        // Create a client for the OrderService using the gRPC channel
        var client = new OrderService.OrderServiceClient(channel);
        
        // Prepare a gRPC request to create an order
        List<ItemP> itemsToSend = ConvertToItemPList(dto.Items); 
        
        {
            CreateOrderP createOrderP = new CreateOrderP()
            {
                AddressId = dto.addressId,
                CustomerUsername = dto.username,
                OrderDate = dto.OrderDate,
                TotalPrice = dto.totalPrice,
                Items = { itemsToSend }
            };
        
        
        
            // Call the gRPC service method asynchronously to create the order
            var response = await client.createOrderAsync(createOrderP);
            
        
            // Shutdown the gRPC channel once the operation is completed
           await  channel.ShutdownAsync();
        
            // Update the domain model Order with the returned order ID
            // order.Id = response.Order.Id;
        }

        // Return a completed task with the original order as a result
    }

    public async Task ConfirmAsync(int orderId)
    {
        
        try
        {
            // Establish a gRPC channel to the server at the specified address
            GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
            // Create a client for the OrderService using the gRPC channel
            var client = new OrderService.OrderServiceClient(channel);
            var request = new OrderIdToConfirm()
            {
                OrderId = orderId
            };
            var response = await client.confirmOrderAsync(request);
        
            await  channel.ShutdownAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<Domain.Models.Order>> GetAllAsync()
    {
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
        var client = new OrderService.OrderServiceClient(channel);
        // Prepare a gRPC request (in this case, an empty request)
        var request = new Google.Protobuf.WellKnownTypes.Empty();
        try
        {
            // Call the gRPC service method to get a stream of items
            var responseStream = client.getAllOrdersAsync(request);
            // Create a list to store the converted domain model items
            var orders = new List<Domain.Models.Order>();
                     
            // Iterate over the response stream asynchronously and convert each item
            foreach (var orderP in responseStream.ResponseAsync.Result.OrdersP)
            {
                var order = new Domain.Models.Order()
                {
                    IsConfirmed = orderP.IsConfirmed,
                    Id = orderP.Id,
                    OrderDate =orderP.OrderDate,
                    DeliveryDate = orderP.DeliveryDate,
                    TotalPrice = orderP.TotalPrice,
                   
                    Customer = new Customer()
                    {
                        FirstName = orderP.Customer.FirstName,
                        Id = orderP.Customer.Id,
                        LastName = orderP.Customer.LastName,
                        UserName = orderP.Customer.Username
                    },
                    Address = new Address()
                    {
                        Country = orderP.Address.Country,
                        City = orderP.Address.City,
                        DoorNumber = orderP.Address.DoorNumber,
                        id = orderP.Address.Id,
                        PostalCode = orderP.Address.PostalCode,
                        State = orderP.Address.State,
                        Street = orderP.Address.Street
                    },
                    Items =ConvertItemPListToDomainModelList(orderP.Items)
                    
                    
                };
                // Add the converted item to the list
                Console.WriteLine(order.Items.FirstOrDefault().quantity);
                orders.Add(order);
            }
            // Return the list of domain model orders
            
            return orders;
        }
        catch (RpcException ex)
        {
            // Handle gRPC exceptions, log the error, and rethrow
            Console.WriteLine($"gRPC error: {ex.Status}");
            throw; 
        }
        finally
        {
          await channel.ShutdownAsync();
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
    

    private OrderP ConvertToOrderP(Domain.Models.Order order)
    {
        // Convert the customer information from domain model to gRPC model
        CustomerP customer = ConvertToCustomerP(order.Customer);
        // Convert the address information from domain model to gRPC model
        AddressP address = ConvertToAddressP(order.Address);
        // Convert the list of items from domain model to a list of gRPC items
        List<ItemP> itemsP = ConvertToItemPList(order.Items);

        // Create and return a new gRPC OrderP object with the converted information
        return new OrderP
        {
            Id = order.Id,
            Customer = customer,
            Items =  {itemsP} ,
            Address = address,
            OrderDate = order.OrderDate,
            DeliveryDate = order.DeliveryDate,
            IsConfirmed = order.IsConfirmed,
            TotalPrice = order.TotalPrice
        };
    }
    
    private CustomerP ConvertToCustomerP(Customer customer)
    {
        // Create a new gRPC AddressP object and populate it with address information from the domain model
        AddressP address = new AddressP
        {
            DoorNumber = customer.Address.DoorNumber,
            Street = customer.Address.Street,
            City = customer.Address.City,
            State = customer.Address.State,
            PostalCode = customer.Address.PostalCode,
            Country = customer.Address.Country
        };
        // Create and return a new gRPC CustomerP object with the converted information
        return new CustomerP
        {
            Username = customer.UserName,
            Password = customer.Password,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Address = address,
            Role = customer.Role
        };
    }

    private AddressP ConvertToAddressP(Domain.Models.Address address)
    {
        // Create and return a new gRPC AddressP object with the converted information
        return new AddressP
        {
            DoorNumber = address.DoorNumber,
            Street = address.Street,
            City = address.City,
            State = address.State,
            PostalCode = address.PostalCode,
            Country = address.Country
        };
    }

    private List<ItemP> ConvertToItemPList(List<Domain.Models.Item> items)
    {
        // Create a new list to store the converted gRPC ItemP objects
        List<ItemP> itemsP = new List<ItemP>();
        
        // Iterate through each item in the input list and convert it to a gRPC ItemP object
        foreach (Domain.Models.Item item in items)
        {
            // Create a new gRPC ItemP object and populate it with information from the domain model item
            ItemP itemP = new ItemP
            {
                ItemId = item.ItemId,
                Name = item.Name,
                Price = item.Price,
                Category = item.Category,
                Stock = item.Stock,
                Description = item.Description,
                Quantity = item.quantity
            };
            // Add the converted item to the list
            itemsP.Add(itemP);
        }
        // Return the list of converted gRPC ItemP objects
        return itemsP;
    }
}